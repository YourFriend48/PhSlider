using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(RectMover))]
[RequireComponent(typeof(Scaler))]
[RequireComponent(typeof(ScoreChanger))]
public class TableString : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _scoreView;
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _starsEffect;

    private RectTransform _rectTransform;
    private RectMover _rectMover;
    private ScoreChanger _scoreChanger;
    private Scaler _scaler;
    private int _score;

    public event Action Scaled;
    public event Action MovementCompleted;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectMover = GetComponent<RectMover>();
        _scoreChanger = GetComponent<ScoreChanger>();
        _scaler = GetComponent<Scaler>();
    }

    private void OnEnable()
    {
        _scoreChanger.Changed += SetScore;
    }

    private void OnDisable()
    {
        _scaler.Completed -= OnScaleCompleted;
        _rectMover.Completed -= OnMovementCompleted;
        _scoreChanger.Changed -= SetScore;
    }

    public void PlayEffect()
    {
        _starsEffect.Play();
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
        _scoreChanger.ChangeScore(_score, target, time);
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
