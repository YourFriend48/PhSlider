using UnityEngine;
using Finance;
using UnityEngine.UI;
using Agava.YandexGames;
using YandexSDK;
using System;

public class Upgrading : MonoBehaviour, IGameSpeedChangable
{
    [SerializeField] private UpgradableFloatParametr _floatParametr;
    [SerializeField] private Price _price;
    [SerializeField] private Button _button;
    [SerializeField] private Button _adsUpgradeButton;
    //[SerializeField] private GameObject _max;

    private WalletHolder _walletHolder;

    public event Action<float> GameSpeedChanged;

    public float FloatParametr => _floatParametr.Value;

    public void Init(WalletHolder walletHolder)
    {
        _walletHolder = walletHolder;
        _price.Init();
        _floatParametr.ExtremumReached += OnExtremumReached;
        _floatParametr.Init();
        Unlock();
        walletHolder.BalanceChanged += OnAccountChanged;
        _button.onClick.AddListener(Upgrade);
        _adsUpgradeButton.onClick.AddListener(OnAdsUpgrade);
    }

    private void OnDisable()
    {
        _floatParametr.ExtremumReached -= OnExtremumReached;
        _walletHolder.BalanceChanged -= OnAccountChanged;
        _button.onClick.RemoveListener(Upgrade);
        _adsUpgradeButton.onClick.RemoveListener(OnAdsUpgrade);
    }

    public void Upgrade()
    {
        int expendeture = _price.Value;
        _price.SetPrice(_price.Value * 2);
        _walletHolder.Withdraw(expendeture);
        _floatParametr.IncreaseParameter();
    }

    public void OnAdsUpgrade()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(onOpenCallback: OnAdOpen, onRewardedCallback: OnRewarded, onCloseCallback: OnAdClose);
#endif
    }

    private void OnAdOpen()
    {
        GameSpeedChanged?.Invoke(0f);
    }

    private void OnRewarded()
    {
        _price.SetPrice(_price.Value * 2);
        _floatParametr.IncreaseParameter();
    }

    private void OnAdClose()
    {
        GameSpeedChanged?.Invoke(1f);
    }

    private void OnExtremumReached()
    {
        _button.gameObject.SetActive(false);
        //_max.gameObject.SetActive(true);
        enabled = false;
    }

    private void OnAccountChanged(int _)
    {
        Unlock();
    }

    private void Unlock()
    {
        if (_price.Value <= _walletHolder.Value)
        {
            _button.interactable = true;
            _button.gameObject.SetActive(true);
            _adsUpgradeButton.gameObject.SetActive(false);
        }
        else
        {
            _button.interactable = false;
            _button.gameObject.SetActive(false);
            _adsUpgradeButton.gameObject.SetActive(true);
        }
    }
}