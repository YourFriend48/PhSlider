using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _lastHitTimeScale = 0.315f;
    [SerializeField] private float _finishTimeScale = 1f;
    [SerializeField] private CameraChanger _cameraChanger;

    private void OnEnable()
    {
        _playerMovement.LastHitInitiated += PlayerMovementOnLastHitInitiated;
        //_playerMovement.FinishReached += PlayerMovementOnFinishReached;
        _cameraChanger.Showed += PlayerMovementOnFinishReached;
        _player.Died += PlayerOnDied;
    }

    private void OnDisable()
    {
        _playerMovement.LastHitInitiated -= PlayerMovementOnLastHitInitiated;
        //_playerMovement.FinishReached -= PlayerMovementOnFinishReached;
        _cameraChanger.Showed -= PlayerMovementOnFinishReached;
        _player.Died -= PlayerOnDied;
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
        Time.timeScale = time;
    }
}