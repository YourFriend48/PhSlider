using UnityEngine;
using General;
using YandexSDK;
using Agava.YandexGames;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Volume _volume;
    //[SerializeField] private LeanLocalizationLoader _localizationLoader;

    private void Start()
    {
        YandexInitializing.Initialized += OnYandexInitialized;
        YandexInitializing.Failed += OnYandexInitializeFailed;

        StartCoroutine(YandexInitializing.Initialize());
    }

    private void OnYandexInitialized()
    {
        Unsubscribe();
        string currentLanguge = YandexGamesSdk.Environment.i18n.lang;
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(currentLanguge);
        StartGame();
    }

    private void OnYandexInitializeFailed()
    {
        Unsubscribe();
        StartGame();
    }

    private void Unsubscribe()
    {
        YandexInitializing.Initialized -= OnYandexInitialized;
        YandexInitializing.Failed -= OnYandexInitializeFailed;
    }

    private void StartGame()
    {
        //GameAnalytics.Initialize();
        _volume.enabled = true;
        //_localizationLoader.enabled = true;
        _levelLoader.enabled = true;
    }
}
