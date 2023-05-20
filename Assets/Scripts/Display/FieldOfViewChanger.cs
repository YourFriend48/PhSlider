using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FieldOfViewChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    [SerializeField] private float _distanceSpeed = 10f;
    [SerializeField] private float _distanceLimit = 65f;

    private bool _isVisible;

    private void Update()
    {
        if (_isVisible || CinemachineCore.Instance.IsLive(_mainCamera) == false)
        {
            return;
        }

        _isVisible = _mainCamera.m_Lens.FieldOfView > _distanceLimit;
        _mainCamera.m_Lens.FieldOfView += Time.deltaTime * _distanceSpeed;
    }

    private void OnBecameVisible()
    {
        _isVisible = true;
    }
}