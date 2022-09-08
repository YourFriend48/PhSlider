using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ActivePlatformsCalculator))]
public class LevelStatusChanger : MonoBehaviour
{
    [SerializeField] private Image _fillArea;
    [SerializeField] private Image _background;
    [SerializeField] private Vector3 _activeScale = new Vector3(1.2f, 1.2f, 1);
    [SerializeField] private Vector3 _inactiveScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float _changeScaleSpeed = 2f;
    [SerializeField] private Color _activeBackground = Color.white;
    [SerializeField] private Color _inactiveBackground = new Color(0.694f, 0.694f, 0.694f);

    private ActivePlatformsCalculator _activePlatformsCalculator;
    private bool _isActive;

    private void Awake()
    {
        _activePlatformsCalculator = GetComponent<ActivePlatformsCalculator>();
    }

    private void OnEnable()
    {
        _activePlatformsCalculator.QuantityChanged += ActivePlatformsCalculator_OnQuantityChanged;
    }

    private void FixedUpdate()
    {
        if (_isActive && transform.localScale != _activeScale)
        {
            ChangeScale(_activeScale);
        }

        if (_isActive == false && transform.localScale != _inactiveScale)
        {
            ChangeScale(_inactiveScale);
        }
    }

    private void OnDisable()
    {
        _activePlatformsCalculator.QuantityChanged -= ActivePlatformsCalculator_OnQuantityChanged;
    }

    private void ActivePlatformsCalculator_OnQuantityChanged(int currentCount, int totalCount)
    {
        if (currentCount == 1)
        {
            _background.color = _activeBackground;
            _isActive = true;
        }

        _fillArea.fillAmount = (float)currentCount / totalCount;

        if (currentCount == totalCount)
        {
            _background.color = _inactiveBackground;
            _isActive = false;
        }
    }

    private void ChangeScale(Vector3 to)
    {
        transform.localScale = Vector3.Lerp(transform.localScale, to, _changeScaleSpeed * Time.deltaTime);
    }
}