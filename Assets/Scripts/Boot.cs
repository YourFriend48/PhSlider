using UnityEngine;
using General;
using YandexSDK;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Volume _volume;

    private void Start()
    {
        YandexInitializing.Initialized += OnYandexInitialized;
        YandexInitializing.Failed += OnYandexInitializeFailed;

        StartCoroutine(YandexInitializing.Initialize());
    }

    private void OnYandexInitialized()
    {
        Unsubscribe();
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
        _levelLoader.enabled = true;
    }
}
