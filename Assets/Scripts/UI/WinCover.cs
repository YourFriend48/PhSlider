using System.Collections;
using UnityEngine;
using General;
using Agava.YandexGames;
using YandexSDK;
using System;

[RequireComponent(typeof(Animator))]
public class WinCover : Screen, IGameSpeedChangable
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _confetti;
    [SerializeField] private float _enableAfterWin = 0.805f;
    [SerializeField] private GemLeaderboard _leaderboard;

    private Animator _animator;

    public event Action<float> GameSpeedChanged;

    private void Start()
    {
        Close();
        _confetti.gameObject.SetActive(false);

        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    protected override void OnButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(onOpenCallback: OnAdOpen,onCloseCallback: OnAdClose);
#endif
        LevelLoader.Instance.LoadNext();
    }

    private void OnAdOpen()
    {
        GameSpeedChanged?.Invoke(0f);
    }

    private void OnAdClose(bool _)
    {
        GameSpeedChanged?.Invoke(1f);
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

    protected override void Disable()
    {
        //_playerMovement.FinishReached -= PlayerMovementOnFinishReached;
    }

    protected override void Enable()
    {
        StartCoroutine(EnableCover());
        //_playerMovement.FinishReached += PlayerMovementOnFinishReached;
        _leaderboard.TryShow();
    }
}