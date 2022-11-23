using Agava.YandexGames;
using Finance;
using System;
using System.Collections;
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

    private bool _isLocked;

    public event Action OldTableFilled;

    private void Awake()
    {
        _entriesWaiting = GetComponent<EntriesWaiting>();
        _isLocked = true;
    }

    private void Start()
    {
        StartCoroutine(Unlock());
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

    private IEnumerator Unlock()
    {
        float yandexRequireWaitTime = 1.1f;
        yield return new WaitForSecondsRealtime(yandexRequireWaitTime);
        _isLocked = false;
    }

    private void Show()
    {
        StartCoroutine(YandexWaiting());
    }

    private IEnumerator YandexWaiting()
    {
        yield return new WaitWhile(() => _isLocked);
        _leaderbordPanel.gameObject.SetActive(true);
        Leaderboard.GetPlayerEntry(LeaderboardName, OnSucsess);
    }

    private void OnSucsess(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        if (leaderboardEntryResponse == null)
        {
            _highestResult = WalletHolder.Instance.Value;
            Leaderboard.SetScore(LeaderboardName, _highestResult);
            Leaderboard.GetEntries(LeaderboardName, CreateStaticTable, null, 0, 5, true);
        }
        else
        {
            _highestResult = Mathf.Max(leaderboardEntryResponse.score, WalletHolder.Instance.Value);
            Leaderboard.SetScore(LeaderboardName, _highestResult);
            Leaderboard.GetEntries(LeaderboardName, CreateStaticTable, null, 0, 5, true);
        }
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

    private void CreateStaticTable(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
    {
        LeaderboardEntryResponse[] table = leaderboardGetEntriesResponse.entries;

        for (int i = 0; i < table.Length; i++)
        {
            LeaderboardEntryResponse entry = table[i];
            Create(entry, i, _startPosition, Vector2.down);
        }
    }
}
