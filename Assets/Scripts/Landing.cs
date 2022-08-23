using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Landing : MonoBehaviour
{
    [SerializeField] private float _startingPositionY = 23.9f;
    [SerializeField] private float _fallSpeed = 16f;
    [SerializeField] private Platform _platform;

    private bool _isLanded;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Vector3 platformCenter = _platform.GetComponent<Collider>().bounds.center;
        transform.position = new Vector3(platformCenter.x, _startingPositionY, platformCenter.z);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isLanded || collision.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _isLanded = true;
    }

    private void Update()
    {
        if (_isLanded)
        {
            return;
        }

        _rigidbody.velocity = Vector3.down * _fallSpeed;
    }
}