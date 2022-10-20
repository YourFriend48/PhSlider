using System;
using UnityEngine;

public class FloatParametr : MonoBehaviour , IFloatParametr
{
    [SerializeField] private float _value;

    public float Value => _value;

    public event Action Upgraded;
    public event Action Setted;
}
