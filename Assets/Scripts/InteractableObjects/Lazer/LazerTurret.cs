using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LazerTurret : MonoBehaviour
{
    [SerializeField] private int _length;
    [SerializeField] private Lazer _lazer;
    [SerializeField] private int _totalTime;
    [SerializeField] private int _startTime;
    [SerializeField] private PlayerMovement _playerMovement;

    private int _currentTime;

    public event Action<int> TimeChanged;

    public int CurrentTime => _currentTime;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        _playerMovement.TurnEnded += OnTurnEnded;
    }

    private void OnDisable()
    {
        _playerMovement.TurnEnded -= OnTurnEnded;
    }

    private void Init()
    {
        Vector3 middlePoint = _lazer.transform.position + (_length - 1) / 2f * _lazer.transform.forward;

        _lazer.transform.position = middlePoint;
        _lazer.transform.localScale = new Vector3(1, 1, _length);

        _currentTime = _startTime;
        TimeChanged?.Invoke(_currentTime);
    }

    private void OnTurnEnded()
    {
        _currentTime--;

        if (_currentTime < 0)
        {
            _currentTime = _totalTime;
        }

        TimeChanged?.Invoke(_currentTime);
    }
}
