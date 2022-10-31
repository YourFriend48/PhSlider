using UnityEngine;
using Finance;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private WalletHolder _walletHolder;
    [SerializeField] private WalletView _walletView;

    [SerializeField] private Upgrading _powerUpgrading;
    [SerializeField] private Upgrading _gemUpgrading;

    private void Start()
    {
        _walletHolder.Init();
        _walletView.Enable();

        _powerUpgrading.Init(_walletHolder);
        _gemUpgrading.Init(_walletHolder);
    }
}