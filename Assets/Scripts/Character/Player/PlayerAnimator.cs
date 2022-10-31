using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Player _player;
    [SerializeField] private Landing _landing;

    private readonly int _flightToIdleHash = Animator.StringToHash("FlightToIdle");
    private readonly int _idleToFlightHash = Animator.StringToHash("IdleToFlight");
    private readonly int _idleToVictoryHash = Animator.StringToHash("IdleToVictory");

    private const string Run = "Run";
    private const string Idle = "Idle";
    private const string Fall = "Fall";
    private const string Kick = "Kick";
    private const string JumpFinished = "JumpFinished";
    //private const string Wo = "Idle";


    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerMovement.Stopped += OnStopped;
        _playerMovement.MovingStarted += OnMovingStarted;
        _player.Won += OnWon;
        _player.Died += OnDied;
        _landing.Landed += OnLanded;
    }

    private void OnDisable()
    {
        _playerMovement.Stopped -= OnStopped;
        _playerMovement.MovingStarted -= OnMovingStarted;
        _player.Won -= OnWon;
        _player.Died -= OnDied;
        _landing.Landed -= OnLanded;
    }

    private void OnLanded()
    {
        _animator.SetTrigger(JumpFinished);
    }

    private void OnDied()
    {
        _animator.SetTrigger(Fall);
    }

    private void OnStopped()
    {
        _animator.SetTrigger(Idle);
    }

    private void OnMovingStarted()
    {
        _animator.SetTrigger(Run);
    }

    private void OnWon()
    {
        _animator.SetTrigger(Kick);
    }

    public void Strike()
    {
        _player.Strike();
    }

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
}