using UnityEngine;
using Agava.YandexGames;
using Finance;

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
    private LeaderboardEntryResponse _oldPlayerResult;
    private TableString _playerString;
    private Vector2 _yourRecordTargetPosition;
    private Vector2 _panelOfOpponentsRecordsEndPosition;

    private void OnDisable()
    {
        if (_playerString != null)
        {
            _playerString.Scaled -= MoveTable;
        }

        _panelOfOpponentsRecords.MovementCompleted -= OnMovementCompleted;
    }

    public void TryShow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
        _leaderbordPanel.gameObject.SetActive(true);
            Leaderboard.GetPlayerEntry(LeaderboardName, onSuccessCallback: OnSucsess);
        }
#endif
        //_leaderbordPanel.gameObject.SetActive(true);
        //CreateFakeTable();
    }

    public void ShowLeaderboard()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
        _leaderbordPanel.gameObject.SetActive(true);
        Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: FillOldTable, onErrorCallback: null, 5, 5, false);
        }
#endif
    }

    public void Hide()
    {
        _leaderbordPanel.gameObject.SetActive(false);
    }

    private void OnSucsess(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        int highestResult;

        if (leaderboardEntryResponse == null)
        {
            highestResult = WalletHolder.Instance.Value;
            Leaderboard.SetScore(LeaderboardName, highestResult);
            //просто показать результат
        }
        else
        {
            _oldPlayerResult = leaderboardEntryResponse;
            highestResult = Mathf.Max(leaderboardEntryResponse.score, WalletHolder.Instance.Value);
            Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: FillOldTable, onErrorCallback: null, 0, 5, true);


            //if(leaderboardEntryResponse != null)
            Leaderboard.SetScore(LeaderboardName, highestResult);
            Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: FillNewTable, onErrorCallback: null, 0, 5, true);
            Animate();
        }
    }

    private void FillOldTableWithoutPlayer(LeaderboardGetEntriesResponse table)
    {
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;

        CreateOldTableWithoutPlayer(leaderboardEntries);
    }

    private void CreateOldTableWithoutPlayer(LeaderboardEntryResponse[] table)
    {
        for (int i = 0; i < table.Length; i++)
        {
            LeaderboardEntryResponse entry = table[i];
            TableString tableString = Create(entry, i, _startPosition, Vector2.down);
        }
    }

    private void FillOldTable(LeaderboardGetEntriesResponse table)
    {
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;

        CreateOldTable(leaderboardEntries, _oldPlayerResult);
    }

    private void FillNewTable(LeaderboardGetEntriesResponse table)//успех
    {
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;

        CreateNewTable(leaderboardEntries, _oldPlayerResult);
    }

    private void CreateOldTable(LeaderboardEntryResponse[] table, LeaderboardEntryResponse leaderboardEntryResponse)
    {
        for (int i = 0; i < table.Length; i++)
        {
            LeaderboardEntryResponse entry = table[i];
            TableString tableString = Create(entry, i, _startPosition, Vector2.down);

            if (table[i].player.uniqueID == leaderboardEntryResponse.player.uniqueID)
            {
                _playerString = tableString;
                _playerString.transform.parent = _resultsWindow.transform;
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

    private void CreateFakeTable()
    {
        for (int i = 0; i < 5; i++)
        {
            FakeCreate(i, _startPosition, Vector2.down);
        }
    }

    private TableString FakeCreate(int index, Vector2 startPosition, Vector2 direction)
    {
        TableString tableString = Instantiate(_tableStringTemplate, _panelOfOpponentsRecords.transform);
        Vector2 rectPosition = startPosition + _offset * index * direction;
        tableString.SetRectPosition(rectPosition);
        return tableString;
    }

    private void CreateNewTable(LeaderboardEntryResponse[] table, LeaderboardEntryResponse leaderboardEntryResponse)
    {
        int positionIndex = 0;

        for (int i = table.Length - 1; i >= 0; i--, positionIndex++)
        {
            LeaderboardEntryResponse entry = table[i];
            TableString tableString = Create(entry, positionIndex, _newRecordsStartPosition, Vector2.up);

            if (table[i].player.uniqueID == leaderboardEntryResponse.player.uniqueID)
            {
                //_yourRecordTargetPosition = _startPosition + 
                tableString.gameObject.SetActive(false);
            }
        }
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
        _panelOfOpponentsRecords.MoveTo(tableStringsCount * _offset * startPosition, _animationTime);
        _panelOfOpponentsRecords.MovementCompleted += OnMovementCompleted;
    }

    private void OnMovementCompleted()
    {
        _panelOfOpponentsRecords.MovementCompleted -= OnMovementCompleted;
        _playerString.ScaleTo(_originalScale);
    }
}
