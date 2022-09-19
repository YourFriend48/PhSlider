using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitArea : MonoBehaviour
{
    [SerializeField] private Transform _positioningObject;

    private Vector3 _lastEulerAngles;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.isKinematic && _lastEulerAngles != _positioningObject.rotation.eulerAngles)
        {
            _rigidbody.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out InactiveRoof _))
        {
            return;
        }

        _rigidbody.isKinematic = true;
        _lastEulerAngles = _positioningObject.rotation.eulerAngles;
    }
}