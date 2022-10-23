using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(RectMover))]
[RequireComponent(typeof(Scaler))]
public class TableString : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _scoreView;
    [SerializeField] private float _speed;

    private RectTransform _rectTransform;
    private RectMover _rectMover;
    private Scaler _scaler;
    private int _score;

    public event Action Scaled;
    public event Action MovementCompleted;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectMover = GetComponent<RectMover>();
        _scaler = GetComponent<Scaler>();
    }

    private void OnDisable()
    {
        _scaler.Completed -= OnScaleCompleted;
        _rectMover.Completed -= OnMovementCompleted;
    }

    public void SetRectPosition(Vector2 position)
    {
        _rectTransform.anchoredPosition = position;
    }

    public void SetName(string text)
    {
        _name.text = text;
    }

    public void SetScore(int count)
    {
        _score = count;
        _scoreView.text = count.ToString();
    }

    public void MoveTo(Vector2 target, float requireTime)
    {
        _rectMover.Completed += OnMovementCompleted;
        _rectMover.MoveTo(target, requireTime);
    }

    public void ScaleTo(Vector3 target)
    {
        _scaler.Completed += OnScaleCompleted;
        _scaler.ScaleTo(target);
    }

    public void ChangeScore(int target, float time)
    {

    }

    private void OnScaleCompleted()
    {
        _scaler.Completed -= OnScaleCompleted;
        Scaled?.Invoke();
    }

    private void OnMovementCompleted()
    {
        _rectMover.Completed -= OnMovementCompleted;
        MovementCompleted?.Invoke();
    }
}
