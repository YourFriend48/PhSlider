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
        //Authorize();
    }

    private void OnFailed()
    {
        StartGame();
    }

    private void Authorize()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
        if (PlayerAccount.IsAuthorized == false)
        {
            PlayerAccount.Authorize(OnPersonalDataRequested);
        }
        else
        {
            StartGame();
        }


        //if (PlayerAccount.IsAuthorized)
        //{
        //    OnSucsessAuthorize();
        //}
        //else
        //{
        //    PlayerAccount.Authorize(OnSucsessAuthorize);
        //}

    }

    private void OnSucsessAuthorize()
    {
        PlayerAccount.RequestPersonalProfileDataPermission(OnPersonalDataRequested);
    }

    private void OnPersonalDataRequested()
    {
        PlayerAccount.GetProfileData(WriteData);
    }

    private void WriteData(PlayerAccountProfileDataResponse data)
    {
        PlayerData.Data = data;
        StartGame();
    }

    private void StartGame()
    {
        //GameAnalytics.Initialize();
        _levelLoader.enabled = true;
    }
}
