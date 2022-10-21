using UnityEngine;
using Agava.YandexGames;
using Finance;
using TMPro;

public class GemLeaderboard : MonoBehaviour
{
    private const string LeaderboardName = "GemLeaderboard";

    [SerializeField] private GameObject _panel;
    [SerializeField] private TableString _tableStringTemplate;
    [SerializeField] private PanelOfOpponentsRecords _panelOfOpponentsRecords;

    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private float _offset;
    [SerializeField] private float _animationTime;
    [SerializeField] private TMP_Text _testText;

    private LeaderboardEntryResponse _oldPlayerResult;
    private TableString _playerString;
    private Vector2 _yourRecordTargetPosition;
    private Vector2 _panelOfOpponentsRecordsEndPosition;

    public void TryShow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.GetPlayerEntry(LeaderboardName, onSuccessCallback: OnSucsess);
        }
#endif
    }

    private void OnSucsess(LeaderboardEntryResponse leaderboardEntryResponse)
    {
        int highestResult;

        if (leaderboardEntryResponse == null)
        {
            highestResult = WalletHolder.Instance.Value;
        }
        else
        {
            _oldPlayerResult = leaderboardEntryResponse;
            highestResult = Mathf.Max(leaderboardEntryResponse.score, WalletHolder.Instance.Value);
            Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: FillOldTable, onErrorCallback: null, 0, 5, true);
        }

        //if(leaderboardEntryResponse != null)
        Leaderboard.SetScore(LeaderboardName, highestResult, onSuccessCallback: SucsessSetScore);
        Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: FillNewTable, onErrorCallback: null, 0, 5, true);
        Show();
    }

    private void SucsessSetScore()//успех
    {
        _testText.text += "SucsessSetScore";
    }

    private void Show()
    {
        _panel.SetActive(true);
        //Animate();
    }

    private void FillOldTable(LeaderboardGetEntriesResponse table)
    {
        _testText.text += "FillOldTable";
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;

        CreateOldTable(leaderboardEntries, _oldPlayerResult);
    }

    private void FillNewTable(LeaderboardGetEntriesResponse table)//успех
    {
        _testText.text += "FillNewTable";
        LeaderboardEntryResponse[] leaderboardEntries = table.entries;

        if (_oldPlayerResult != null)
        {
            CreateNewTable(leaderboardEntries, _oldPlayerResult);
        }
    }

    private void CreateOldTable(LeaderboardEntryResponse[] table, LeaderboardEntryResponse leaderboardEntryResponse)
    {
        for (int i = 0; i < table.Length; i++)
        {
            TableString tableString = Create(table, i);
            _testText.text += tableString.transform.localPosition;

            if (table[i].player.uniqueID == leaderboardEntryResponse.player.uniqueID)
            {
                _playerString = tableString;
                _playerString.transform.parent = _panel.transform;
            }
        }
    }

    private TableString Create(LeaderboardEntryResponse[] table, int index)
    {
        TableString tableString = Instantiate(_tableStringTemplate, _panelOfOpponentsRecords.transform);
        Vector2 rectPosition = _startPosition + _offset * index * Vector2.down;
        tableString.SetRectPosition(rectPosition);
        tableString.SetName(table[index].player.publicName);
        tableString.SetScore(table[index].score.ToString());
        _testText.text += tableString.transform.localPosition.ToString();
        return tableString;
    }

    private void CreateNewTable(LeaderboardEntryResponse[] table, LeaderboardEntryResponse leaderboardEntryResponse)
    {
        for (int i = table.Length - 1; i >= 0; i--)
        {
            TableString tableString = Create(table, i);

            if (table[i].player.uniqueID == leaderboardEntryResponse.player.uniqueID)
            {
                tableString.gameObject.SetActive(false);
            }
        }
    }

    private void Animate()
    {
        // Но сначала увеличить кнопку

        _playerString.MoveTo(_yourRecordTargetPosition, _animationTime);
        _panelOfOpponentsRecords.MoveTo(_panelOfOpponentsRecordsEndPosition, _animationTime);

        //Затем уменьшить кнопку
    }
}
