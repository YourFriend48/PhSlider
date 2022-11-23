using UnityEngine;
using Finance;
using General;
using GameAnalyticsSDK;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private WalletHolder _walletHolder;
    [SerializeField] private WalletView _walletView;

    [SerializeField] private Upgrading _powerUpgrading;
    [SerializeField] private Upgrading _gemUpgrading;

    [SerializeField] private Volume _volume;

    private VolumeButton _volumeButton;

    //[SerializeField] private PoolManager _poolManager;

    private void Start()
    {
        GameAnalytics.Initialize();
        // _poolManager.Init();
        //EventsSender.Instance.SendLevelStartEvent();
        _walletHolder.Init();
        _walletView.Enable();

        _powerUpgrading.Init(_walletHolder);
        _gemUpgrading.Init(_walletHolder);

        _volumeButton = FindObjectOfType<VolumeButton>();

        _volumeButton.Init(_volume);
    }
}