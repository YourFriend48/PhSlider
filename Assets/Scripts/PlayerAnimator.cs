using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly int _flightToIdleHash = Animator.StringToHash("FlightToIdle");
    private readonly int _idleToFlightHash = Animator.StringToHash("IdleToFlight");

    private Animator _animator;

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