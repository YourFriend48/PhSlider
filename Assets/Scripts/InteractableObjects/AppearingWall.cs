using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AppearingWall : InactiveRoof
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Switcher _button;
    [SerializeField] private TowardsMover _model;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        if (_button.IsOn)
        {
            OnButtonOn();
        }
        else
        {
            OnButtonOff();
        }

        _button.Off += OnButtonOff;
        _button.On += OnButtonOn;
    }

    private void OnButtonOff()
    {
        _collider.enabled = false;
        _model.MoveTowards(_startPosition.position);
    }

    private void OnButtonOn()
    {
        _collider.enabled = true;
        _model.MoveTowards(_endPosition.position);
    }
}
