using UnityEngine.Events;
using UnityEngine;
using System;

namespace Finance
{
    public class WalletHolder : MonoBehaviour
    {
        private Wallet _wallet;

        public event UnityAction<int> BalanceChanged;

        public static WalletHolder Instance { get; private set; }

        public int Value => _wallet.Value;

        public void Init()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                throw new InvalidOperationException($"Level may have single instance of {nameof(WalletHolder)}");
            }

            _wallet = new Wallet();
            _wallet.BalanceChanged += OnBalanceChanged;
        }

        private void OnDisable()
        {
            _wallet.BalanceChanged -= OnBalanceChanged;
        }

        public void PutIn(int value)
        {
            _wallet.PutIn(value);
        }

        public void Withdraw(int value)
        {
            _wallet.Withdraw(value);
        }

        private void OnBalanceChanged(int value)
        {
            BalanceChanged?.Invoke(value);
        }
    }
}