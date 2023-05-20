using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YandexSDK;

public static class LeaderboardData
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

    public static void WriteData(List<LeaderboardEntryResponse> entries)
    {
        List<LeaderboardEntryResponse> _opponentData = entries;

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

    public static List<LeaderboardEntryResponse> GetData()
    {
        int index = 0;

        for (int i = 0; i < _opponentData.Count; i++)
        {
            if (_playerData.score >= _opponentData[i].score)
            {
                index = i;
                break;
            }
        }

        return GetList(index);
    }

    private static List<LeaderboardEntryResponse> GetList(int index)
    {
        List<LeaderboardEntryResponse> entries = new List<LeaderboardEntryResponse>();

        TryAddElementByIndex(index - 2, entries);
        TryAddElementByIndex(index - 1, entries);
        entries.Add(_playerData);
        TryAddElementByIndex(index, entries);
        TryAddElementByIndex(index + 1, entries);

        return entries;
    }

    private static void TryAddElementByIndex(int index, List<LeaderboardEntryResponse> entries)
    {
        LeaderboardEntryResponse firstElement = GetElement(index - 2);

        if (firstElement != null)
        {
            entries.Add(firstElement);
        }
    }

    private static LeaderboardEntryResponse GetElement(int index)
    {
        if (index >= 0 && index < _opponentData.Count)
        {
            return _opponentData[index];
        }
        else
        {
            return null;
        }
    }
}
