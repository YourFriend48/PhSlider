using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _soundOnImage;
    [SerializeField] private Image _soundOffImage;

    private bool _isOn;

    private void OnEnable()
    {
        OnSetted(Volume.Instance.Value);
        _button.onClick.AddListener(OnChanged);
    }

    private void OnDisable()
    {
        OnSetted(Volume.Instance.Value);
        _button.onClick.RemoveListener(OnChanged);
    }

    private void OnSetted(float value)
    {
        _isOn = value == 0;
        Switch();
    }

    private void Switch()
    {
        _isOn = !_isOn;

        _soundOnImage.gameObject.SetActive(_isOn);
        _soundOffImage.gameObject.SetActive(!_isOn);
    }

    private void OnChanged()
    {
        if (_isOn)
        {
            Volume.Instance.VolumeOff();
        }
        else
        {
            Volume.Instance.VolumeOn();
        }

        Switch();
    }
}
