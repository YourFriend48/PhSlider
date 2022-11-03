using UnityEngine;
using Finance;
using Pooling;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private WalletHolder _walletHolder;
    [SerializeField] private WalletView _walletView;

    [SerializeField] private Upgrading _powerUpgrading;
    [SerializeField] private Upgrading _gemUpgrading;

    //[SerializeField] private PoolManager _poolManager;

    private void Start()
    {
        // _poolManager.Init();

        _walletHolder.Init();
        _walletView.Enable();

        _powerUpgrading.Init(_walletHolder);
        _gemUpgrading.Init(_walletHolder);

    }
}