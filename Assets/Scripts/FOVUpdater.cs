using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FOVUpdater : MonoBehaviour
{
    [SerializeField] private Camera _target;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _camera.fieldOfView = _target.fieldOfView;
    }
}
