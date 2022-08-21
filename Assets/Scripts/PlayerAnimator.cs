using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly int _flightToIdleHash = Animator.StringToHash("FlightToIdle");
    private readonly int _idleHash = Animator.StringToHash("Idle");
    private readonly int _idleToFlightHash = Animator.StringToHash("IdleToFlight");

    private Animator _animator;

    public bool IsIdle()
    {
        AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return currentAnimatorStateInfo.shortNameHash == _idleHash;
    }

    public void RunFlightToIdle()
    {
        _animator.SetTrigger(_flightToIdleHash);
    }

    public void RunIdleToFlight()
    {
        _animator.SetTrigger(_idleToFlightHash);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}