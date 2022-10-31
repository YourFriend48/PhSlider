using Agava.YandexGames;
using Finance;
using System;
using UnityEngine;
using YandexSDK;

[RequireComponent(typeof(EntriesWaiting))]
public class GemLeaderboard : MonoBehaviour
{
    private const string LeaderboardName = "GemLeaderboard";

    [SerializeField] private GameObject _leaderbordPanel;
    [SerializeField] private TableString _tableStringTemplate;
    [SerializeField] private Transform _resultsWindow;
    [SerializeField] private PanelOfOpponentsRecords _panelOfOpponentsRecords;

    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _newRecordsStartPosition;
    [SerializeField] private float _offset;
    [SerializeField] private float _animationTime;

    [SerializeField] private float _targetScaleMultiplier;

    private Vector3 _originalScale;
    private Vector3 _targetScale;
    private TableString _playerString;
    private LeaderboardEntryResponse[] _tableAtStartLevel;
    private EntriesWaiting _entriesWaiting;
    private int _highestResult;

    public event Action OldTableFilled;

    private void Awake()
    {
        _entriesWaiting = GetComponent<EntriesWaiting>();
    }

    private void OnEnable()
    {
        YandexAuthorizing.Authorized += OnSucsessAuthorize;
        YandexPersonalData.DataPermissioned += Show;
    }

    private void OnDisable()
    {
        YandexAuthorizing.Authorized -= OnSucsessAuthorize;
        YandexPersonalData.DataPermissioned -= Show;

        OldTableFilled -= OnOldTableFilled;
        _entriesWaiting.Completed -= CreateNewTable;
        _entriesWaiting.Completed -= CreateOldTable;
        _panelOfOpponentsRecords.MovementCompleted -= OnMovementCompleted;

        if (_playerString != null)
        {
            _playerString.Scaled -= OnScaleDownComleted;
            _playerString.Scaled -= OnScaleUpCompleted;
            _playerString.Scaled -= MoveTable;
        }
    }

    public void TryShow()
    {
        if (YandexAuthorizing.GetIsAuthorized())
        {
            if (YandexPersonalData.GetDataIsAvailabled() == false)
            {
                YandexPersonalData.RequestData();
            }

            Show();
        }
        else
        {
            YandexAuthorizing.Authorize();
        }
    }

    private void OnSucsessAuthorize()
    {
        TryShow();
    }

    private void WriteStartData(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
    {
        _tableAtStartLevel = leaderboardGetEntriesResponse.entries;
    }

    private void Show()
    {
        _leaderbordPanel.gameObject.SetActive(true);
        Leaderboard.GetPlayerEntry(LeaderboardName, OnSucsess);
    }

    private void OnSucsess(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        if (leaderboardEntryResponse == null)
        {
            _highestResult = WalletHolder.Instance.Value;
            Leaderboard.SetScore(LeaderboardName, _highestResult);
            Leaderboard.GetEntries(LeaderboardName, WriteStartData, null, 0, 5, true);
            CreateOldTable(_tableAtStartLevel);
        }
        else
        {
            _highestResult = Mathf.Max(leaderboardEntryResponse.score, WalletHolder.Instance.Value);
            OldTableFilled += OnOldTableFilled;
            Leaderboard.GetEntries(LeaderboardName, FillOldTable, null, 0, 5, true);
        }
    }

    private void OnOldTableFilled()
    {
        OldTableFilled -= OnOldTableFilled;
        Leaderboard.SetScore(LeaderboardName, _highestResult);
        Leaderboard.GetEntries(LeaderboardName, FillNewTable, null, 0, 5, true);
    }

    private void FillNewTable(LeaderboardGetEntriesResponse table)
    {
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;
        _entriesWaiting.Completed += CreateNewTable;
        _entriesWaiting.Wait(leaderboardEntries);
    }

    private void FillOldTable(LeaderboardGetEntriesResponse table)
    {
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;
        _entriesWaiting.Completed += CreateOldTable;
        _entriesWaiting.Wait(leaderboardEntries);
    }

    private void CreateOldTable(LeaderboardEntryResponse[] table)
    {
        _entriesWaiting.Completed -= CreateOldTable;

        for (int i = 0; i < table.Length; i++)
        {
            LeaderboardEntryResponse entry = table[i];
            TableString tableString = Create(entry, i, _startPosition, Vector2.down);

            if (entry.player.uniqueID == YandexPersonalData.Data.uniqueID)
            {
                _playerString = tableString;
                _playerString.transform.parent = _resultsWindow;
                tableString.GetComponent<UnityEngine.UI.Image>().maskable = false;
            }
        }

        OldTableFilled?.Invoke();
    }

    private TableString Create(LeaderboardEntryResponse entry, int index, Vector2 startPosition, Vector2 direction)
    {
        TableString tableString = Instantiate(_tableStringTemplate, _panelOfOpponentsRecords.transform);
        Vector2 rectPosition = startPosition + _offset * index * direction;
        tableString.SetRectPosition(rectPosition);

        if (entry.player.publicName == "")
        {
            tableString.SetName("Anonymous");
        }
        else
        {
            tableString.SetName(entry.player.publicName);
        }

        tableString.SetScore(entry.score);
        return tableString;
    }

    private void CreateNewTable(LeaderboardEntryResponse[] table)
    {
        _entriesWaiting.Completed -= CreateNewTable;
        int positionIndex = 0;

        for (int i = table.Length - 1; i >= 0; i--, positionIndex++)
        {
            LeaderboardEntryResponse entry = table[i];

            if (table[i].player.uniqueID != YandexPersonalData.Data.uniqueID)
            {
                TableString tableString = Create(entry, positionIndex, _newRecordsStartPosition, Vector2.up);
            }
        }

        Animate();
    }

    private void Animate()
    {
        _originalScale = _playerString.transform.localScale;
        _targetScale = _originalScale * _targetScaleMultiplier;
        _playerString.Scaled += OnScaleUpCompleted;
        _playerString.ScaleTo(_targetScale);
    }

    private void OnScaleUpCompleted()
    {
        _playerString.Scaled -= OnScaleUpCompleted;
        _playerString.ChangeScore(_highestResult, _animationTime);
        MoveTable();
    }

    private void MoveTable()
    {
        int tableStringsCount = 15;
        Vector2 startPosition = _panelOfOpponentsRecords.GetComponent<RectTransform>().anchoredPosition;
        _panelOfOpponentsRecords.MoveTo(tableStringsCount * _offset * Vector2.down + startPosition, _animationTime);
        _panelOfOpponentsRecords.MovementCompleted += OnMovementCompleted;
    }

    private void OnMovementCompleted()
    {
        _panelOfOpponentsRecords.MovementCompleted -= OnMovementCompleted;
        _playerString.Scaled += OnScaleDownComleted;
        _playerString.ScaleTo(_originalScale);
    }

    private void OnScaleDownComleted()
    {
        _playerString.Scaled -= OnScaleDownComleted;
        _playerString.PlayEffect();
    }
}
