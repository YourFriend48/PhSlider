using UnityEngine;
using Agava.WebUtility;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _lastHitTimeScale = 0.315f;
    [SerializeField] private float _finishTimeScale = 1f;
    [SerializeField] private CameraChanger _cameraChanger;

    private float _timeScale;

    private void OnEnable()
    {
        _playerMovement.LastHitInitiated += PlayerMovementOnLastHitInitiated;
        _cameraChanger.Showed += PlayerMovementOnFinishReached;
        _player.Died += PlayerOnDied;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        WinCover.AdOpened += OnAdOpened;
        WinCover.AdClosed += OnAdClosed;
        Upgrading.AdOpened += OnAdOpened;
        Upgrading.AdClosed += OnAdClosed;
    }

    private void OnDisable()
    {
        _playerMovement.LastHitInitiated -= PlayerMovementOnLastHitInitiated;
        _cameraChanger.Showed -= PlayerMovementOnFinishReached;
        _player.Died -= PlayerOnDied;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        WinCover.AdOpened -= OnAdOpened;
        WinCover.AdClosed -= OnAdClosed;
        Upgrading.AdOpened -= OnAdOpened;
        Upgrading.AdClosed -= OnAdClosed;
    }

    private void OnAdOpened()
    {
        SetTimeScale(0);
    }

    private void OnAdClosed()
    {
        SetTimeScale(1);
    }

    private void PlayerMovementOnFinishReached()
    {
        SetTimeScale(_finishTimeScale);
    }

    private void PlayerMovementOnLastHitInitiated()
    {
        SetTimeScale(_lastHitTimeScale);
    }

    private void PlayerOnDied()
    {
        SetTimeScale(_finishTimeScale);
    }

    private void SetTimeScale(float time)
    {
        _timeScale = time;
        Time.timeScale = time;
    }

    private void OnInBackgroundChange(bool isInBackground)
    {
        if (isInBackground)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = _timeScale;
        }
    }
}