using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private float _lastHitTimeScale = .315f;
    [SerializeField] private float _finishTimeScale = 1f;

    private void OnEnable()
    {
        _movement.LastHitInitiated += Movement_OnLastHitInitiated;
        _movement.FinishReached += Movement_OnFinishReached;
    }

    private void OnDisable()
    {
        _movement.LastHitInitiated -= Movement_OnLastHitInitiated;
        _movement.FinishReached -= Movement_OnFinishReached;
    }

    private void Movement_OnFinishReached()
    {
        Time.timeScale = _finishTimeScale;
    }

    private void Movement_OnLastHitInitiated()
    {
        Time.timeScale = _lastHitTimeScale;
    }
}