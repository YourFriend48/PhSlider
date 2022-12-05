using Agava.YandexMetrica;
using Finance;
using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YandexMetricaIntegration : MonoBehaviour
{
    private void OnEnable()
    {
        OnLevelStart(SceneManager.GetActiveScene().buildIndex);
        WinCover.ComplitedLevel += OnLevelComplete;
        GameCover.Reloaded += OnRestartLevel;
        LoseCover.Failed += OnLevelFail;
        InvulnerabilityView.LookedAd += SkipLevelAdOffer;
        Upgrading.UpgradedForGems += UpgradeForGems;
        Upgrading.UpgradedForAd += UpgradeForAd;
    }

    private void OnDisable()
    {
        WinCover.ComplitedLevel -= OnLevelComplete;
        GameCover.Reloaded -= OnRestartLevel;
        LoseCover.Failed -= OnLevelFail;
        InvulnerabilityView.LookedAd -= SkipLevelAdOffer;
        Upgrading.UpgradedForGems -= UpgradeForGems;
        Upgrading.UpgradedForAd -= UpgradeForAd;
    }

    public void OnLevelStart(int numberLevel)
    {
        YandexMetrica.Send($"level {numberLevel} Start");
        Debug.Log($"OnLevelStart + level {numberLevel} Start");
    }

    public void OnLevelComplete()
    {
        YandexMetrica.Send($"level {SceneManager.GetActiveScene().buildIndex} Complete");
        Debug.Log($"level {SceneManager.GetActiveScene().buildIndex} Complete");
    }

    public void OnLevelFail(int numberLevel)
    {
        YandexMetrica.Send($"level {numberLevel} Fail", $"{{\"Fail Time\": \"{Time.timeSinceLevelLoad}\"}}");
        Debug.Log($"level {numberLevel} Fail");
    }

    public void OnRestartLevel(int numberLevel)
    {
        YandexMetrica.Send($"level {numberLevel} Restart");
        Debug.Log($"level {numberLevel} Restart");
    }

    public void SkipLevelAdOffer(int numberLevel)
    {
        YandexMetrica.Send($"skipLevelAdOffer {numberLevel}");
        Debug.Log($"skipLevelAdOffer {numberLevel}");
    }

    public void UpgradeForGems(string name)
    {
        YandexMetrica.Send($"Upgrade for Gems {name}");
        Debug.Log($"Upgrade for Gems {name}");
    }

    public void UpgradeForAd(string name)
    {
        YandexMetrica.Send($"Upgrade for Ad {name}");
        Debug.Log($"Upgrade for Ad {name}");
    }
}
