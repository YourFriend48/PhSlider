using UnityEngine;
using Finance;
using YandexSDK;
using Agava.YandexGames;


public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private WalletHolder _walletHolder;
    [SerializeField] private WalletView _walletView;

    [SerializeField] private Upgrading _powerUpgrading;

    private void Start()
    {
        _walletHolder.Init();
        _walletView.Enable();

        _powerUpgrading.Init(_walletHolder);


        //PlayerAccount.RequestPersonalProfileDataPermission();
        //if (!PlayerAccount.IsAuthorized)
        //    PlayerAccount.Authorize(OnPersonalDataRequested);
    }
    private void OnPersonalDataRequested()
    {
        PlayerAccount.GetProfileData(WriteData);
    }

    private void WriteData(PlayerAccountProfileDataResponse data)
    {
        PlayerData.Data = data;
    }
}
