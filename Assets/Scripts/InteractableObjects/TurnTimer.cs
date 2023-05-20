using UnityEngine;
using System;

public class TurnTimer : MonoBehaviour
{
    [SerializeField] private int _totalTime;
    [SerializeField] private int _startTime;
    [SerializeField] private PlayerMovement _playerMovement;

    private int _currentTime;

    public event Action<int> TimeChanged;

    public int CurrentTime => _currentTime;

    private void OnEnable()
    {
        _currentTime = _startTime;
        TimeChanged?.Invoke(_currentTime);
        _playerMovement.TurnEnded += OnTurnEnded;
    }

    private void OnDisable()
    {
        _playerMovement.TurnEnded -= OnTurnEnded;
    }

    private void OnTurnEnded()
    {
        Tick();
    }

    private void Tick()
    {
        _currentTime--;

        if (_currentTime < 0)
        {
            _currentTime = _totalTime;
        }

        TimeChanged?.Invoke(_currentTime);
    }
}
