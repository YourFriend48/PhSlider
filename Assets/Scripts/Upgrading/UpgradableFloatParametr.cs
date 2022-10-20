using UnityEngine;
using System;

public class UpgradableFloatParametr : MonoBehaviour, IFloatParametr
{
    [SerializeField, Min(0)] private float _value;
    [SerializeField, Min(0)] private float _upgradeStep;
    [SerializeField, Min(0)] private float _extremumValue;
    [SerializeField] private string _id;

    private FloatSerializable _floatSerializable;
    private Action _targetFunction;

    public event Action Upgraded;
    public event Action Setted;
    public event Action ExtremumReached;

    public float NextValue { get; private set; }

    public float Value => _value;
    public float ExtremumValue => _extremumValue;

    public void Init()
    {
        if (_value > _extremumValue)
        {
            _targetFunction = () => SetLowerValue(_value, _upgradeStep, _extremumValue);
        }
        else
        {
            _targetFunction = () => SetHigherValue(_value, _upgradeStep, _extremumValue);
        }

        if (PlayerPrefs.HasKey(_id))
        {
            _floatSerializable = PlayerPrefsSaver<FloatSerializable>.Load(_id);
            _value = _floatSerializable.Value;
            CheckOnMinValue();
        }
        else
        {
            _floatSerializable = new FloatSerializable();
        }

        _targetFunction.Invoke();
        Setted?.Invoke();
    }

    public void IncreaseParameter()
    {
        _value = NextValue;
        _targetFunction.Invoke();
        Upgraded?.Invoke();

        _floatSerializable.Value = _value;
        PlayerPrefsSaver<FloatSerializable>.Save(_id, _floatSerializable);
        CheckOnMinValue();
    }

    private void CheckOnMinValue()
    {
        if (_value == _extremumValue)
        {
            ExtremumReached?.Invoke();
        }
    }

    private void SetLowerValue(float value, float upgradeStep, float extremumValue)
    {
        float newValue = value - upgradeStep;

        if (newValue > extremumValue)
        {
            NextValue = newValue;
        }
        else
        {
            NextValue = extremumValue;
        }
    }

    private void SetHigherValue(float value, float upgradeStep, float extremumValue)
    {
        float newValue = value + upgradeStep;

        if (newValue < extremumValue)
        {
            NextValue = newValue;
        }
        else
        {
            NextValue = extremumValue;
        }
    }
}