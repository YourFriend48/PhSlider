using System.Collections;
using UnityEngine;
using General;
using Agava.YandexGames;
using YandexSDK;
using System;

[RequireComponent(typeof(Animator))]
public class WinCover : Screen, IGameSpeedGhangable
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _confetti;
    [SerializeField] private float _enableAfterWin = 0.805f;

    private Animator _animator;
    private bool _isAdShowed = false;

    public event Action<float> GameSpeedChanged;
    //public Action<bool> AddClosed;

    //private void Awake()
    //{
    //    AddClosed = (bool flag) => OnAdClose(true);
    //}

    private void OnEnable()
    {
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
        OnEnableBase();
    }

    private void Start()
    {
        Close();
        _confetti.gameObject.SetActive(false);

        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void OnDisable()
    {
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
        OnDisableBase();
    }

    protected override void OnButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(onOpenCallback: OnAdOpen,onCloseCallback: OnAdClose);
#endif
        if(_isAdShowed == false)
        {
        LevelLoader.Instance.LoadNext();
        }
    }

    private void OnAdOpen()
    {
        _isAdShowed = true;
        GameSpeedChanged?.Invoke(0f);
    }

    private void OnAdClose(bool _)
    {
        GameSpeedChanged?.Invoke(1f);
        _isAdShowed = false;
        LevelLoader.Instance.LoadNext();
    }

    private IEnumerator EnableCover()
    {
        yield return new WaitForSeconds(_enableAfterWin);

        Open();
        _animator.enabled = true;
        _confetti.gameObject.SetActive(true);
    }

    private void PlayerMovementOnFinishReached()
    {
        StartCoroutine(EnableCover());
    }
}