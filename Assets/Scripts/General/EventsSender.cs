//using GameAnalyticsSDK;
using UnityEngine;

namespace General
{
    public class EventsSender : MonoBehaviour
    {
        //private IYandexAppMetrica _appMetrica;

        //public static EventsSender Instance;

        //private void Awake()
        //{
        //    Instance = this;
        //}

        //public void Init()
        //{
        //    GameAnalytics.Initialize();
        //    _appMetrica = AppMetrica.Instance;
        //}

        //public void SendGameStartEvent(int sessions)
        //{
        //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Game");
        //    _appMetrica.ReportEvent("game_start", $"{{\"count\":\"{sessions}\"}}");
        //}

        //public void SendCustomUserEvent(int sessions, int soft, string registrationDay, int daysInGame)
        //{
        //    _appMetrica.ReportEvent(
        //        "custom_user",
        //        $"{{\"sessions_count\":\"{sessions}\", \"current_soft\":\"{soft}\", \"reg_day\":\"{registrationDay}\", \"days_in_game\":\"{daysInGame}\"}}");
        //}

        //public void SendLevelStartEvent(int level)
        //{
        //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, level.ToString());
        //    _appMetrica.ReportEvent("level_start", $"{{\"level\":\"{level}\"}}");
        //}

        //public void SendLevelCompleteEvent(int level, int seconds)
        //{
        //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level {level}");
        //    _appMetrica.ReportEvent("level_complete", $"{{\"level\":\"{level}\", \"time_spent\":\"{seconds}\"}}");
        //}

        //public void SendLevelFailEvent(int level, int seconds)
        //{
        //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level {level}");
        //    _appMetrica.ReportEvent("fail", $"{{\"level\":\"{level}\", \"time_spent\":\"{seconds}\"}}");
        //}

        //public void SendSoftSpentEvent(string type, string name, int amount, int count)
        //{
        //    GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Money", amount, type, name);
        //    _appMetrica.ReportEvent(
        //        "soft_spent",
        //        $"{{\"type\":\"{type}\", \"name\":\"{name}\", \"amount\":\"{amount}\", \"count\":\"{count}\"}}");
        //}
    }
}