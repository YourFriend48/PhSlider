using GameAnalyticsSDK;
using System;
using UnityEngine;

namespace General
{
    public class EventsSender : MonoBehaviour
    {
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

        public void SendGameStartEvent(int sessions)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game", sessions);
        }

        public void SendCustomUserEvent(int sessions, int soft, string registrationDay, int daysInGame)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "RegistrationDay", registrationDay, "daysInGame", daysInGame);
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