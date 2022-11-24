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

    public event Action OldTableFilled;

    private void Awake()
    {
        _entriesWaiting = GetComponent<EntriesWaiting>();
    }

    private void OnDisable()
    {
        YandexAuthorizing.Authorized -= OnSucsessAuthorize;
        YandexPersonalData.DataLoaded -= Show;
    }

    public void TryShow()
    {
        Debug.Log("TryShow");

        if (YandexPersonalData.IsDataSetted)
        {
            Show();
        }
        else
        {
            if (YandexAuthorizing.IsAuthorised == false)
            {
                YandexAuthorizing.Authorized += OnSucsessAuthorize;
                YandexAuthorizing.Authorise();
            }
            else
            {
                Show();
            }
        }
    }

    private void OnSucsessAuthorize()
    {
        YandexAuthorizing.Authorized -= OnSucsessAuthorize;
        YandexPersonalData.DataLoaded += Show;
        YandexPersonalData.Request();
    }

    private void Show()
    {
        YandexPersonalData.DataLoaded -= Show;
        _leaderbordPanel.gameObject.SetActive(true);

        if (YandexPersonalData.HasLeaderboardRecord)
        {
            UpdateRecord(YandexPersonalData.HighestResult);
        }
        else
        {
            Leaderboard.GetPlayerEntry(LeaderboardName, OnSucsess);
        }
    }

    private void OnSucsess(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        YandexPersonalData.HasLeaderboardRecord = true;
        Debug.Log("OnSucsess");
        if (leaderboardEntryResponse == null)
        {
            YandexPersonalData.HighestResult = WalletHolder.Instance.Value;
            Leaderboard.SetScore(LeaderboardName, YandexPersonalData.HighestResult, OnScoreSetted);
        }
        else
        {
            UpdateRecord(leaderboardEntryResponse.score);
        }
    }

    private void WriteData()
    {
        //Leaderboard.GetEntries(LeaderboardName, LeaderboardData.WriteData(), null, 0, 5, true);
    }

    //private void CreateStaticTable(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
    //{
    //    Debug.Log("CreateStaticTable");
    //    LeaderboardEntryResponse[] table = leaderboardGetEntriesResponse.entries;

    //    for (int i = 0; i < table.Length; i++)
    //    {
    //        LeaderboardEntryResponse entry = table[i];
    //        Create(entry, i, _startPosition, Vector2.down);
    //    }
    //}

    private void UpdateRecord(int previousRecord)
    {
        YandexPersonalData.HighestResult = Mathf.Max(previousRecord, WalletHolder.Instance.Value);
        Leaderboard.SetScore(LeaderboardName, YandexPersonalData.HighestResult, OnScoreSetted);
    }

    private void OnScoreSetted()
    {
        Debug.Log("OnScoreSetted");
        //Leaderboard.GetEntries(LeaderboardName, CreateStaticTable, null, 0, 5, true);
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
        Debug.Log("CreateStaticTable");
        LeaderboardEntryResponse[] table = leaderboardGetEntriesResponse.entries;

        for (int i = 0; i < table.Length; i++)
        {
            LeaderboardEntryResponse entry = table[i];
            Create(entry, i, _startPosition, Vector2.down);
        }
    }
}
