using UnityEngine;
using System;

[RequireComponent(typeof(RectMover))]
public class PanelOfOpponentsRecords : MonoBehaviour
{
    private RectMover _rectMover;

    public event Action MovementCompleted;

    private void Awake()
    {
        _rectMover = GetComponent<RectMover>();
    }

    private void OnDisable()
    {
        _rectMover.Completed -= OnMovementCompleted;
    }

    public void MoveTo(Vector2 target, float requireTime)
    {
        _rectMover.Completed += OnMovementCompleted;
        _rectMover.MoveTo(target, requireTime);
    }

    private void OnMovementCompleted()
    {
        _rectMover.Completed -= OnMovementCompleted;
        MovementCompleted?.Invoke();
    }
}
