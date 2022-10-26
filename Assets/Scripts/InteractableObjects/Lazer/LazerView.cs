using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LazerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private LazerTurret _lazerTurret;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        OnTimeChanged(_lazerTurret.CurrentTime);
        _lazerTurret.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _lazerTurret.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(int value)
    {
        if (value == 0)
        {
            _image.gameObject.SetActive(false);
        }
        else
        {
            _image.gameObject.SetActive(true);
            _text.text = value.ToString();
        }
    }
}
