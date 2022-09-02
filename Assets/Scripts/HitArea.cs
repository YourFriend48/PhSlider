using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitArea : MonoBehaviour
{
    [SerializeField] private RoofDetector _roofDetector;
    [SerializeField] private Transform _positioningObject;

    private Vector3 _currentEulerAngles;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _roofDetector.Faced += RoofDetectorOnFaced;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.isKinematic && _currentEulerAngles != _positioningObject.rotation.eulerAngles)
        {
            _rigidbody.isKinematic = false;
        }
    }

    private void OnDisable()
    {
        _roofDetector.Faced -= RoofDetectorOnFaced;
    }

    private void RoofDetectorOnFaced()
    {
        _rigidbody.isKinematic = true;
        _currentEulerAngles = _positioningObject.rotation.eulerAngles;
    }
}