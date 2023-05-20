using UnityEngine;
using UnityEngine.Events;

public static class LeaderboardScore
{
    public static int Value { get; private set; }

    public static event UnityAction<int> Changed;

    private static string _saveKey = "Score";

    public static void AddScore(int value)
    {
        if (value > 0)
        {
            SetScore(Value + value);
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(_saveKey, Value);
    }

    public static void Load()
    {
        SetScore(PlayerPrefs.GetInt(_saveKey, 0));
    }

    private static void SetScore(int score)
    {
        if (score > Value)
        {
            Value = score;
            Changed?.Invoke(Value);
        }
    }
}