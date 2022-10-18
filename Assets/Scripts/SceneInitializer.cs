using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Finance;
using PlayerComponents;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private WalletHolder _walletHolder;
    [SerializeField] private WalletView _walletView;

    [SerializeField] private SwipeInput _swipeInput;
    [SerializeField] private Map _map;
    [SerializeField] private PlayerComponents.PlayerMovement _playerMovement;

    private void Start()
    {
        _walletHolder.Init();
        _walletView.Enable();
        _map.Init();
        _playerMovement.Init(_map, _swipeInput);
    }
}
