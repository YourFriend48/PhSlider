using UnityEngine;
using System;

[RequireComponent(typeof(LerpScaler))]
public class IoIoScaler : MonoBehaviour
{
    [SerializeField] private float _requireTime;

    private LerpScaler _lerpScaler;
    private Vector3 _startScale;

    public event Action Completed;

    private void Awake()
    {
        _lerpScaler = GetComponent<LerpScaler>();
        _startScale = transform.localScale;
    }

    private void OnDisable()
    {
        _lerpScaler.Completed -= OnComleted;
        _lerpScaler.Completed -= OnCicleEnd;
    }

    public void Scale(Vector3 target)
    {
        _lerpScaler.ScaleLerp(target, _requireTime);
        _lerpScaler.Completed += OnComleted;
    }

    private void OnComleted()
    {
        _lerpScaler.Completed -= OnComleted;
        _lerpScaler.Completed += OnCicleEnd;
        _lerpScaler.ScaleLerp(_startScale, _requireTime);
    }

    private void OnCicleEnd()
    {
        _lerpScaler.Completed -= OnCicleEnd;
        Completed?.Invoke();
    }
}
