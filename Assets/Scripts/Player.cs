using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private bool _isLanded;

    public event UnityAction Landed;
    public event UnityAction SteppedFinishPlatform;
    public event UnityAction SteppedLastHitPlatform;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform) == false)
        {
            return;
        }

        if (_isLanded == false)
        {
            _isLanded = true;
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