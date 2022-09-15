using UnityEngine;

public class PowerCanvas : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _lastWorldPosition;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        Quaternion cameraRotation = _camera.transform.rotation;
        Vector3 worldPosition = transform.position + (cameraRotation * Vector3.forward);

        if (worldPosition == _lastWorldPosition)
        {
            return;
        }

        transform.LookAt(worldPosition, cameraRotation * Vector3.up);
        _lastWorldPosition = worldPosition;
    }
}