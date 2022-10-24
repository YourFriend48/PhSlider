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
        Authorize();
    }

    private void OnFailed()
    {
        StartGame();
    }

    private void Authorize()
    {
        if (PlayerAccount.IsAuthorized)
        {
            OnSucsessAuthorize();
        }
        else
        {
            PlayerAccount.Authorize(OnSucsessAuthorize);
        }

        StartGame();
    }

    private void OnSucsessAuthorize()
    {
        PlayerAccount.GetProfileData(WriteData);
    }

    private void WriteData(PlayerAccountProfileDataResponse data)
    {
        PlayerData.Data = data;
    }

    private void StartGame()
    {
        //GameAnalytics.Initialize();
        _levelLoader.enabled = true;
    }
}
