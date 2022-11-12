using UnityEngine;
using General;
using YandexSDK;
using Agava.YandexGames;
using System.Collections;
using GameAnalyticsSDK;
using General;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Volume _volume;
    //[SerializeField] private LungageSetter _lungageSetter;

    private void Start()
    {
        YandexInitializing.Initialized += OnYandexInitialized;
        YandexInitializing.Failed += OnYandexInitializeFailed;

        StartCoroutine(YandexInitializing.Initialize());
    }

    private void OnYandexInitialized()
    {
        Unsubscribe();
        StartCoroutine(GetEnvironment());
    }

    private IEnumerator GetEnvironment()
    {
        YandexGamesEnvironment env = YandexGamesSdk.Environment;

        yield return new WaitWhile(() => env == null);
        Languge.Name = env.i18n.lang;
        StartGame();
    }

    private void OnYandexInitializeFailed()
    {
        Unsubscribe();
        //Languge.Name = "tr";
        StartGame();
    }

    private void Unsubscribe()
    {
        YandexInitializing.Initialized -= OnYandexInitialized;
        YandexInitializing.Failed -= OnYandexInitializeFailed;
    }

    private void StartGame()
    {
        GameAnalytics.Initialize();
        EventsSender.Instance.SendStartEvents();
        _volume.enabled = true;
        _levelLoader.enabled = true;
    }
}
