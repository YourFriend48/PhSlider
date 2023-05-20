using Agava.YandexGames;
using UnityEngine;
using System.Collections;
using System;

public class EntriesWaiting : MonoBehaviour
{
    public event Action<LeaderboardEntryResponse[]> Completed;

    public void Wait(LeaderboardEntryResponse[] leaderboardEntries)
    {
        StartCoroutine(WaitWhileGetEntries(leaderboardEntries));
    }

    private IEnumerator WaitWhileGetEntries(LeaderboardEntryResponse[] leaderboardEntries)
    {
        yield return new WaitUntil(() => CheckLeaderboardEntryResponse(leaderboardEntries));
        Completed?.Invoke(leaderboardEntries);
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
}
