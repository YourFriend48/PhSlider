using TMPro;
using UnityEngine;

public class PowerVisualizer : MonoBehaviour
{
    [SerializeField] private Power _power;
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private Vector3 _increaseTextScale = new Vector3(1.8f, 1.8f, 1.8f);
    [SerializeField] private float _increaseTextSpeed = 8f;
    [SerializeField] private Scaler _scaler;
    [SerializeField] private Vector3 _originalScale = Vector3.one;

    private void OnEnable()
    {
        PowerOnChanged(_power.Current);
        _power.Changed += PowerOnChanged;
    }

    private void OnDisable()
    {
        _power.Changed -= PowerOnChanged;
        _scaler.Completed -= OnScaleCompleted;
    }

    private void OnScaleCompleted()
    {
        _scaler.Completed -= OnScaleCompleted;
        _scaler.ScaleTo(_originalScale);
    }

    private void PowerOnChanged(int value)
    {
        _displayText.text = value.ToString();
        _scaler.Completed -= OnScaleCompleted;
        _scaler.Completed += OnScaleCompleted;
        _scaler.ScaleTo(_increaseTextScale);
    }
}