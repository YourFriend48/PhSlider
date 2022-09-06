using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitArea : MonoBehaviour
{
    [SerializeField] private Transform _positioningObject;

    private Vector3 _currentEulerAngles;
    private Rigidbody _rigidbody;

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

        Vector3 origin = _positioningObject.position;
        bool raycast = Physics.Raycast(
            origin,
            _positioningObject.TransformDirection(Vector3.forward),
            out RaycastHit raycastHit,
            Mathf.Infinity);

        if (raycast == false || raycastHit.collider.TryGetComponent(out Roof _) == false)
        {
            return;
        }

        _rigidbody.isKinematic = true;
        _currentEulerAngles = _positioningObject.rotation.eulerAngles;
    }
}