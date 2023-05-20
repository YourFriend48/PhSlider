using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LazerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TurnTimer _turnTimer;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        OnTimeChanged(_turnTimer.CurrentTime);
        _turnTimer.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _turnTimer.TimeChanged -= OnTimeChanged;
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
