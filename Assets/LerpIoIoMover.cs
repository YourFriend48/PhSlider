using UnityEngine;
using System;

[RequireComponent(typeof(LerpMover))]
public class LerpIoIoMover : MonoBehaviour
{
    [SerializeField] private float _requireTime;

    private LerpMover _lerpMover;
    private Vector3 _startPosition;

    public event Action Completed;

    private void Awake()
    {
        _lerpMover = GetComponent<LerpMover>();
        _startPosition = transform.position;
    }

    private void OnDisable()
    {
        _lerpMover.Completed -= OnComleted;
        _lerpMover.Completed -= OnCicleEnd;
    }

    public void Move(Vector3 target)
    {
        _lerpMover.MoveLerp(target, _requireTime);
        _lerpMover.Completed += OnComleted;
    }

    private void OnComleted()
    {
        _lerpMover.Completed -= OnComleted;
        _lerpMover.Completed += OnCicleEnd;
        _lerpMover.MoveLerp(_startPosition, _requireTime);
    }

    private void OnCicleEnd()
    {
        _lerpMover.Completed -= OnCicleEnd;
        Completed?.Invoke();
    }
}
