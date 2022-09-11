using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _lastHitTimeScale = 0.315f;
    [SerializeField] private float _finishTimeScale = 1f;

    private void OnEnable()
    {
        _playerMovement.LastHitInitiated += PlayerMovementOnLastHitInitiated;
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
    }

    private void OnDisable()
    {
        _playerMovement.LastHitInitiated -= PlayerMovementOnLastHitInitiated;
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
    }

    private void PlayerMovementOnFinishReached()
    {
        Time.timeScale = _finishTimeScale;
    }

    private void PlayerMovementOnLastHitInitiated()
    {
        Time.timeScale = _lastHitTimeScale;
    }
}