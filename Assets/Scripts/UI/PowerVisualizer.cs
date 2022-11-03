using TMPro;
using UnityEngine;

public class PowerVisualizer : MonoBehaviour
{
    [SerializeField] private Power _power;
    [SerializeField] private TMP_Text _displayText;
    [SerializeField] private Vector3 _increaseTextScale = new Vector3(1.8f, 1.8f, 1.8f);
    [SerializeField] private Scaler _scaler;
    [SerializeField] private Vector3 _originalScale = Vector3.one;

    private void Start()
    {
        PowerOnSetted(_power.Current);
        _power.Setted += PowerOnSetted;
    }

    private void OnEnable()
    {
        _power.Changed += PowerOnChanged;
    }

    private void OnDisable()
    {
        _power.Setted -= PowerOnSetted;
        _power.Changed -= PowerOnChanged;
        _scaler.Completed -= OnScaleCompleted;
        _scaler.Completed -= OnDisappeared;
        _scaler.Completed -= OnAppeared;
    }

    public void Appear()
    {
        _scaler.Completed += OnAppeared;
        _scaler.ScaleTo(_originalScale);
    }

    public void Disappear()
    {
        _scaler.Completed += OnDisappeared;
        _scaler.ScaleTo(Vector3.zero);
    }

    private void OnDisappeared()
    {
        _scaler.Completed -= OnDisappeared;
    }

    private void OnAppeared()
    {
        _scaler.Completed -= OnAppeared;
    }

    private void OnScaleCompleted()
    {
        _scaler.Completed -= OnScaleCompleted;
        _scaler.ScaleTo(_originalScale);
    }

    private void PowerOnChanged(int value)
    {
        _displayText.text = value.ToString();
        _scaler.Completed += OnScaleCompleted;
        _scaler.ScaleTo(_increaseTextScale);
    }

    private void PowerOnSetted(int value)
    {
        _power.Setted -= PowerOnSetted;
        _displayText.text = value.ToString();
    }
}