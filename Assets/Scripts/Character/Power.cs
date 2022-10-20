using UnityEngine;
using System;

public class Power : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _floatParametr;
    
    private IFloatParametr _iFloatParametr => (IFloatParametr)_floatParametr;
    private int _current;

    public event Action<int> Changed;

    public int Current => _current;

    private void OnValidate()
    {
        if (_floatParametr != null && _floatParametr is not IFloatParametr)
        {
            Debug.LogError(nameof(_floatParametr) + " needs to implement " + nameof(IFloatParametr));
            _floatParametr = null;
        }
    }

    private void OnEnable()
    {
        OnUpgraded();
        _iFloatParametr.Setted += OnSetted;
        _iFloatParametr.Upgraded+= OnUpgraded;
    }

    private void OnDisable()
    {
        _iFloatParametr.Upgraded -= OnUpgraded;
        _iFloatParametr.Setted -= OnSetted;
    }

    private void OnSetted()
    {
        _current = (int)_iFloatParametr.Value;
        Changed?.Invoke(_current);
    }

    public void Increase()
    {
        _current++;
        Changed?.Invoke(_current);
    }
    private void OnUpgraded()
    {
        _current = (int)_iFloatParametr.Value;
        Changed?.Invoke(_current);
    }
}