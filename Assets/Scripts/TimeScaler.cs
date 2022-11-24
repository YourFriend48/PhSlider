using UnityEngine;
using Agava.WebUtility;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _lastHitTimeScale = 0.315f;
    [SerializeField] private float _finishTimeScale = 1f;
    [SerializeField] private CameraChanger _cameraChanger;

    private float _timeScale = 1f;

    public float LastHitTimeScale => _lastHitTimeScale;

    private void OnEnable()
    {
        _player.Won += PlayerMovementOnLastHitInitiated;
        _cameraChanger.Showed += PlayerMovementOnFinishReached;
        _player.Died += PlayerOnDied;
        _player.Failed += PlayerOnDied;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        WinCover.AdOpened += OnAdOpened;
        WinCover.AdClosed += OnAdClosed;
        Upgrading.AdOpened += OnAdOpened;
        Upgrading.AdClosed += OnAdClosed;
        InvulnerabilityView.Showed += OnAdOpened;
        InvulnerabilityView.Closed += OnAdClosed;
    }

    private void OnDisable()
    {
        _player.Won -= PlayerMovementOnLastHitInitiated;
        _cameraChanger.Showed -= PlayerMovementOnFinishReached;
        _player.Died -= PlayerOnDied;
        _player.Failed -= PlayerOnDied;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        WinCover.AdOpened -= OnAdOpened;
        WinCover.AdClosed -= OnAdClosed;
        Upgrading.AdOpened -= OnAdOpened;
        Upgrading.AdClosed -= OnAdClosed;
        InvulnerabilityView.Showed -= OnAdOpened;
        InvulnerabilityView.Closed -= OnAdClosed;
    }

    private void OnAdOpened()
    {
        SetTimeScale(0);
    }

    private void OnAdClosed()
    {
        Debug.Log("OnAdClosed");
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
        Debug.Log("time " + time);
        _timeScale = time;
        Debug.Log("_timeScale " + _timeScale);
        Time.timeScale = time;
        Debug.Log("Time.timeScale " + Time.timeScale);
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