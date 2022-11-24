using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YandexSDK;

public class LeaderboardData : MonoBehaviour
{
    public static List<LeaderboardEntryResponse> _opponentData;
    public static LeaderboardEntryResponse _playerData;

    public static void WriteData(LeaderboardGetEntriesResponse leaderboardGetEntriesResponse)
    {
        List<LeaderboardEntryResponse> _opponentData = leaderboardGetEntriesResponse.entries.ToList();

        foreach (LeaderboardEntryResponse entry in _opponentData)
        {
            if (entry.player.uniqueID == YandexPersonalData.Data.uniqueID)
            {
                _playerData = entry;
                _opponentData.Remove(entry);
                break;
            }
        }
    }

    //public static List<LeaderboardEntryResponse> GetData()
    //{
    //    int index;

    //    for(int i =0; i< _opponentData.Count; i++)
    //    {
    //        if(_playerData.score >= _opponentData[i].score)
    //        {
    //            index = i;
    //            break;
    //        }
    //    }
    //}

    //private void GetList(int index)
    //{
    //    player
    //    List<LeaderboardEntryResponse> entries = new List<LeaderboardEntryResponse>();
    //    for(int i=0; i <2;i++)
    //    {

    //    }
    //}

    //private LeaderboardEntryResponse GetElement(int index)
    //{
    //    if(index>=0 && index< _opponentData.Count)
    //    {
    //        return _opponentData[index];
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
}
