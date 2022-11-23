using System.Collections;
using UnityEngine;
using General;
using Agava.YandexGames;
using YandexSDK;
using System;
using General;
using GameAnalyticsSDK;

[RequireComponent(typeof(Animator))]
public class WinCover : EndScreen
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _confetti;
    [SerializeField] private float _enableAfterWin = 0.805f;
    [SerializeField] private GemLeaderboard _leaderboard;

    private Animator _animator;

    public static event Action<float> GameSpeedChanged;
    public static event Action AdOpened;
    public static event Action AdClosed;

    protected override void OnButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(onOpenCallback: OnAdOpen,onCloseCallback: OnAdClose);
#endif
        LevelLoader.Instance.LoadNext();
    }

    private void OnAdOpen()
    {
        EventsSender.Instance.SendAdEvent(GAAdAction.Clicked, GAAdType.Interstitial);
        AdOpened?.Invoke();
    }

    private void OnAdClose(bool _)
    {
        AdClosed?.Invoke();
    }

    private void Win()
    {
        Open();
        _animator.enabled = true;
        _confetti.gameObject.SetActive(true);
        _leaderboard.TryShow();
    }

    protected override void Disable()
    {
    }

    protected override void Enable()
    {
        Win();
        //StartCoroutine(EnableCover());
    }

    protected override void OnAwake()
    {
        _animator = GetComponent<Animator>();
    }
}