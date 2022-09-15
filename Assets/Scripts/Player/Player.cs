using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private bool _isLanded;
    private Rigidbody _rigidbody;

    public event UnityAction Died;
    public event UnityAction Landed;

    public void Die()
    {
        Died?.Invoke();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isLanded || collision.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _isLanded = true;
        _rigidbody.isKinematic = true;

        Landed?.Invoke();
    }
}