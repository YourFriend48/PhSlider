using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly int _flightToIdleHash = Animator.StringToHash("FlightToIdle");
    private readonly int _idleToFlightHash = Animator.StringToHash("IdleToFlight");
    private readonly int _idleToVictoryHash = Animator.StringToHash("IdleToVictory");

    private Animator _animator;

    public void RunFlightToIdle()
    {
        _animator.SetBool(_flightToIdleHash, true);
    }

    public void RunIdleToFlight()
    {
        _animator.SetBool(_flightToIdleHash, false);
        _animator.SetTrigger(_idleToFlightHash);
    }

    public void RunIdleToVictory()
    {
        _animator.SetTrigger(_idleToVictoryHash);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}