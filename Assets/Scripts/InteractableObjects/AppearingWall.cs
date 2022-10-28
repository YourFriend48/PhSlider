using UnityEngine;

public class AppearingWall : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Switcher _button;
    [SerializeField] private TowardsMover _model;

    public bool IsRaised { get; private set; }

    public Vector3 Center => _center.position;

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
        IsRaised = false;
        _model.MoveTowards(_startPosition.localPosition);
    }

    private void OnButtonOn()
    {
        IsRaised = true;
        _model.MoveTowards(_endPosition.localPosition);
    }
}
