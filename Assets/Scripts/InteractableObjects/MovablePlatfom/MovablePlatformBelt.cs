using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatformBelt : MonoBehaviour
{
    [SerializeField] private NavigationPlatform[] _platforms;
    [SerializeField] private NavigationType _type;
    [SerializeField] private MovablePlatform _template;
    [SerializeField] private Transform _container;
    [SerializeField] private PlayerMovement _playerMovement;

    private List<MovablePlatform> _models;

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

    public void Init()
    {
        _models = new List<MovablePlatform>();

        for (int i = 0; i < _platforms.Length; i++)
        {
            if (_platforms[i].IsMovable)
            {
                MovablePlatform movablePlatform = Instantiate(_template, _container);
                movablePlatform.transform.position = _platforms[i].Center;
                _models.Add(movablePlatform);
                movablePlatform.PositionIndex = i;
            }
        }
    }

    private void UpdateNavigationAttribute()
    {
        bool firstNavigationAttribute = _platforms[0].IsMovable;

        for (int i = 0; i < _platforms.Length - 1; i++)
        {
            _platforms[i].IsMovable = _platforms[i + 1].IsMovable;
        }

        _platforms[_platforms.Length - 1].IsMovable = firstNavigationAttribute;
    }

    private void OnTurnEnded()
    {
        UpdateNavigationAttribute();
        UpdateView();
    }

    private void UpdateView()
    {
        foreach (MovablePlatform movablePlatform in _models)
        {
            int positionIndex = GetNextPositionIndex(movablePlatform.PositionIndex);
            movablePlatform.PositionIndex = positionIndex;
            Vector3 target = _platforms[positionIndex].Center;
            movablePlatform.Move(target);
        }
    }

    private int GetNextPositionIndex(int current)
    {
        current++;

        if (current == _platforms.Length)
        {
            current = 0;
        }

        return current;
    }

    enum NavigationType
    {
        Circle
    }
}
