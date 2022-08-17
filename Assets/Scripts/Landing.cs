using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class Landing : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    [SerializeField] private float _startingPositionY = 25f;
    [SerializeField] private float _fallSpeed = 10f;

    private bool _isLanded;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Vector3 platformCenter;

        if (_platform.TryGetComponent(out Renderer platformRenderer))
        {
            platformCenter = platformRenderer.bounds.center;
        }
        else
        {
            var sumVector = new Vector3(0f, 0f, 0f);

            sumVector = _platform.transform.Cast<Transform>().Aggregate(
                sumVector,
                (current, child) => current + child.position);

            platformCenter = sumVector / _platform.transform.childCount;
        }

        transform.position = new Vector3(platformCenter.x, _startingPositionY, platformCenter.z);
    }

    private void Update()
    {
        if (_isLanded)
        {
            return;
        }

        _rigidbody.velocity = Vector3.down * _fallSpeed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isLanded || collision.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _isLanded = true;
        Landed?.Invoke();
    }

    public event UnityAction Landed;
}