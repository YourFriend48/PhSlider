using UnityEngine;
using UnityEngine.Events;

public class ActivePlatformsCalculator : MonoBehaviour
{
    [SerializeField] private ActiveBuildingArea _roofActiveArea;
    [SerializeField] private Material _platformMaterial;

    private int _activePlatformCount;
    private bool _isDisabled;
    private ColorablePlatformPart[] _platformParts;

    public event UnityAction<int, int> QuantityChanged;

    private void Start()
    {
        _platformParts = _roofActiveArea.GetComponentsInChildren<ColorablePlatformPart>();
    }

    private void Update()
    {
        if (_isDisabled)
        {
            return;
        }

        int activePlatformCount = 0;

        foreach (ColorablePlatformPart colorablePart in _platformParts)
        {
            if (colorablePart.TryGetComponent(out MeshRenderer meshRenderer) == false
                || meshRenderer.sharedMaterial != _platformMaterial)
            {
                continue;
            }

            activePlatformCount++;
        }

        if (_activePlatformCount >= activePlatformCount)
        {
            return;
        }

        _activePlatformCount = activePlatformCount;
        QuantityChanged?.Invoke(_activePlatformCount, _platformParts.Length);

        if (_activePlatformCount == _platformParts.Length)
        {
            _isDisabled = true;
        }
    }
}