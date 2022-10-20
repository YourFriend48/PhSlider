using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using YandexSDK;
using Agava.YandexGames;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private YandexInitializer _yandexInitializer;

    private void Start()
    {
        _yandexInitializer.Initialized += OnYandexInitialized;
        _yandexInitializer.Failed += OnFailed;
        _yandexInitializer.Init();
    }

    private void OnYandexInitialized()
    {
        StartGame();
    }

    private void OnFailed()
    {
        StartGame();
    }

    private void Authorize()
    {
        if (PlayerAccount.IsAuthorized)
        {
            //OnSucsessAuthorize();
        }
        else
        {
            PlayerAccount.Authorize();
        }

        StartGame();
    }

    private void OnSucsessAuthorize()
    {
        //we can use leaderboard        
    }

    private void StartGame()
    {
        //GameAnalytics.Initialize();
        //_integrationMetric.OnGameStart();
        _levelLoader.enabled = true;
        //_integrationMetric.SetUserProperty();
    }
}
