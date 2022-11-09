using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        //Volume.Instance.Setted += OnSetted;
        _slider.onValueChanged.AddListener(OnChanged);
    }

    private void OnDisable()
    {
        //Volume.Instance.Setted -= OnSetted;
        _slider.onValueChanged.RemoveListener(OnChanged);
    }

    private void OnSetted(float value)
    {
        _slider.value = value;
    }

    private void OnChanged(float value)
    {

    }
}
