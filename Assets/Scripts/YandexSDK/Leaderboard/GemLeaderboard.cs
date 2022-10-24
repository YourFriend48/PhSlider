using UnityEngine;
using Agava.YandexGames;
using Finance;
using System.Collections;

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

    [SerializeField] private MouseInput _mouseInput;

    private Vector3 _originalScale;
    private Vector3 _targetScale;
    private TableString _playerString;
    private LeaderboardEntryResponse[] _tableAtStartLevel;

    private void OnEnable()
    {
        _mouseInput.Swipped += OnSwipped;
    }

    private void OnDisable()
    {
        if (_playerString != null)
        {
            _playerString.Scaled -= MoveTable;
        }

        _mouseInput.Swipped -= OnSwipped;
        _panelOfOpponentsRecords.MovementCompleted -= OnMovementCompleted;
    }

    public void TryShow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(PlayerData.Data!=null)
        {
        Show();
                }
#endif
    }

    private void OnSwipped()
    {
        _mouseInput.Swipped -= OnSwipped;
#if UNITY_WEBGL && !UNITY_EDITOR
        if(PlayerData.Data!=null)
        {
        _text.text += " PlayerData.Data!=null ";
        Leaderboard.GetEntries(LeaderboardName, WriteStartData, null, 0, 5, true);
        }
#endif
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
        int highestResult;

        if (leaderboardEntryResponse == null)
        {
            highestResult = WalletHolder.Instance.Value;
            Leaderboard.SetScore(LeaderboardName, highestResult);
            Leaderboard.GetEntries(LeaderboardName, WriteStartData, null, 0, 5, true);
            CreateOldTable(_tableAtStartLevel);
        }
        else
        {
            highestResult = Mathf.Max(leaderboardEntryResponse.score, WalletHolder.Instance.Value);
            CreateOldTable(_tableAtStartLevel);
            Leaderboard.SetScore(LeaderboardName, highestResult);
            Leaderboard.GetEntries(LeaderboardName, FillNewTable, null, 0, 5, true);
        }
    }

    private void FillNewTable(LeaderboardGetEntriesResponse table)//call twice?
    {
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;
        StartCoroutine(WaitWhileGetEntries(leaderboardEntries));
    }

    private IEnumerator WaitWhileGetEntries(LeaderboardEntryResponse[] leaderboardEntries)
    {
        yield return new WaitUntil(() => CheckLeaderboardEntryResponse(leaderboardEntries));
        CreateNewTable(leaderboardEntries);
    }

    private bool CheckLeaderboardEntryResponse(LeaderboardEntryResponse[] leaderboardEntries)
    {
        foreach (LeaderboardEntryResponse entry in leaderboardEntries)
        {
            if (entry == null)
            {
                return false;
            }
        }

        return true;
    }

    private void CreateOldTable(LeaderboardEntryResponse[] table)
    {
        for (int i = 0; i < table.Length; i++)
        {
            LeaderboardEntryResponse entry = table[i];
            TableString tableString = Create(entry, i, _startPosition, Vector2.down);

            if (entry.player.uniqueID == PlayerData.Data.uniqueID)
            {
                _playerString = tableString;
                _playerString.transform.parent = _resultsWindow;
            }
        }
    }

    private TableString Create(LeaderboardEntryResponse entry, int index, Vector2 startPosition, Vector2 direction)
    {
        TableString tableString = Instantiate(_tableStringTemplate, _panelOfOpponentsRecords.transform);
        Vector2 rectPosition = startPosition + _offset * index * direction;
        tableString.SetRectPosition(rectPosition);
        tableString.SetName(entry.player.publicName);
        tableString.SetScore(entry.score);
        return tableString;
    }

    private void CreateNewTable(LeaderboardEntryResponse[] table)
    {
        int positionIndex = 0;

        for (int i = table.Length - 1; i >= 0; i--, positionIndex++)
        {
            LeaderboardEntryResponse entry = table[i];

            if (table[i].player.uniqueID != PlayerData.Data.uniqueID)
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
        _playerString.Scaled += MoveTable;
        _playerString.ScaleTo(_targetScale);
    }

    private void MoveTable()
    {
        _playerString.Scaled -= MoveTable;
        int tableStringsCount = 15;
        Vector2 startPosition = _panelOfOpponentsRecords.GetComponent<RectTransform>().anchoredPosition;
        _panelOfOpponentsRecords.MoveTo(tableStringsCount * _offset * Vector2.down + startPosition, _animationTime);
        _panelOfOpponentsRecords.MovementCompleted += OnMovementCompleted;
    }

    private void OnMovementCompleted()
    {
        _panelOfOpponentsRecords.MovementCompleted -= OnMovementCompleted;
        _playerString.ScaleTo(_originalScale);
    }
}
