using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private TowardsMover _model;
    [SerializeField] private Switcher _switcher;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _redMaterial;

    private void OnEnable()
    {
        if (_switcher.IsOn)
        {
            OnButtonOn();
        }
        else
        {
            OnButtonOff();
        }

        _switcher.On += OnButtonOn;
        _switcher.Off += OnButtonOff;
    }

    private void OnDisable()
    {
        _switcher.On -= OnButtonOn;
        _switcher.Off -= OnButtonOff;
    }

    private void OnButtonOn()
    {
        _model.MoveTowards(_startPosition.localPosition);
        _meshRenderer.material = _greenMaterial;
    }

    private void OnButtonOff()
    {
        _model.MoveTowards(_endPosition.localPosition);
        _meshRenderer.material = _redMaterial;
    }
}
