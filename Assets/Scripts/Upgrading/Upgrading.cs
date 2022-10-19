using UnityEngine;
using Finance;
using UnityEngine.UI;

public class Upgrading : MonoBehaviour
{
    [SerializeField] private UpgradableFloatParametr _floatParametr;
    [SerializeField] private Price _price;
    [SerializeField] private Button _button;
    //[SerializeField] private GameObject _max;

    private WalletHolder _walletHolder;

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
    }

    private void OnDisable()
    {
        _floatParametr.ExtremumReached -= OnExtremumReached;
        _walletHolder.BalanceChanged -= OnAccountChanged;
        _button.onClick.RemoveListener(Upgrade);
    }

    public void Upgrade()
    {
        int expendeture = _price.Value;
        _price.SetPrice(_price.Value * 2);
        _walletHolder.Withdraw(expendeture);
        _floatParametr.IncreaseParameter();
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
        }
        else
        {
            _button.interactable = false;
        }
    }
}