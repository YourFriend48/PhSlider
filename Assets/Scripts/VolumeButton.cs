using UnityEngine;
using UnityEngine.UI;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _soundOnImage;
    [SerializeField] private Image _soundOffImage;

    private Volume _volume;
    private bool _isOn;

    //private void OnEnable()
    //{
    //    OnSetted(_volume.Value);
    //    _button.onClick.AddListener(OnChanged);
    //}

    private void OnDisable()
    {
        OnSetted(_volume.Value);
        _button.onClick.RemoveListener(OnChanged);
    }

    public void Init(Volume volume)
    {
        _volume = volume;
        OnSetted(_volume.Value);
        _button.onClick.AddListener(OnChanged);
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
            _volume.VolumeOff();
        }
        else
        {
            _volume.VolumeOn();
        }

        Switch();
    }
}
