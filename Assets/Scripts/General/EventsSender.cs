using GameAnalyticsSDK;
using System;
using UnityEngine;

namespace General
{
    public class EventsSender : MonoBehaviour
    {
        private const string RegistrationDateKey = nameof(RegistrationDateKey);
        private const string SessionsKey = nameof(SessionsKey);

        public static EventsSender Instance;

        private DateTime _startLevelDateTime;

        private void Awake()
        {
            Instance = this;
        }

        public void Init()
        {
            GameAnalytics.Initialize();
        }

        public void SendStartEvents()
        {
            int sessions;
            int daysInGame;
            string registrationDate;

            if (PlayerPrefs.HasKey(SessionsKey))
            {
                sessions = PlayerPrefs.GetInt(SessionsKey);
                sessions++;

                registrationDate = PlayerPrefs.GetString(RegistrationDateKey);
            }
            else
            {
                sessions = 0;

                registrationDate = DateTime.Now.ToString();
                PlayerPrefs.SetString(RegistrationDateKey, registrationDate);
            }

            daysInGame = DateTime.Now.Subtract(DateTime.Parse(registrationDate)).Days;
            PlayerPrefs.SetInt(SessionsKey, sessions);
            SendGameStartEvent(sessions);
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "RegistrationDay", registrationDate, "daysInGame", daysInGame);
        }

        public void SendGameStartEvent(int sessions)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game", sessions);
        }

        public void SendLevelStartEvent(int level)
        {
            _startLevelDateTime = DateTime.UtcNow;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level.ToString());
        }

        public void SendLevelCompleteEvent(int level)
        {
            int seconds = DateTime.UtcNow.Subtract(_startLevelDateTime).Seconds;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level {level}", seconds);

        }

        public void SendLevelRestartEvent(int level)
        {
            int seconds = DateTime.UtcNow.Subtract(_startLevelDateTime).Seconds;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Restart {level}", seconds);

        }

        public void SendProgressionEvent(string message)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, message);

        }

        public void SendLevelFailEvent(int level)
        {
            int seconds = DateTime.UtcNow.Subtract(_startLevelDateTime).Seconds;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level {level}", seconds);
        }

        public void SendSoftSpentEvent(string type, string name, int amount, int count)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Money", amount, type, name);

        }

        public void SendAdEvent(GAAdAction action, GAAdType type)
        {
            GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Video, "adSdkName", "adPlacement");
        }
    }
}