using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private bool _isLanded;
    private Rigidbody _rigidbody;

    public event UnityAction Landed;
    public event UnityAction SteppedFinishPlatform;
    public event UnityAction SteppedLastHitPlatform;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform) == false)
        {
            return;
        }

        if (_isLanded == false)
        {
            _isLanded = true;

            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY
                                     | RigidbodyConstraints.FreezeRotationX
                                     | RigidbodyConstraints.FreezeRotationY
                                     | RigidbodyConstraints.FreezeRotationZ;

            Landed?.Invoke();
            return;
        }

        if (platform.TryGetComponent(out LastHitPlatform _))
        {
            SteppedLastHitPlatform?.Invoke();
            return;
        }

        if (platform.TryGetComponent(out FinishPlatform _))
        {
            SteppedFinishPlatform?.Invoke();
        }
    }
}