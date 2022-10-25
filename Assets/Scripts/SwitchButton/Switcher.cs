using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Switcher : MonoBehaviour
{
    [SerializeField] private bool _isOn;
    [SerializeField] private Trigger _trigger;

    public bool IsOn => _isOn;

    public event Action Off;
    public event Action On;

    private void OnEnable()
    {
        _trigger.Enter += OnEnter;

        if (_isOn)
        {
            On?.Invoke();
        }
        else
        {
            Off?.Invoke();
        }
    }

    private void OnDisable()
    {
        _trigger.Enter -= OnEnter;
    }

    private void OnEnter(Collider collider)
    {
        if (collider.GetComponent<Player>())
        {
            Switch();
        }
    }

    private void Switch()
    {
        _isOn = !_isOn;

        if (_isOn)
        {
            On?.Invoke();
        }
        else
        {
            Off?.Invoke();
        }
    }
}
