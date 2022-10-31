using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Player _player;
    [SerializeField] private Landing _landing;
    [SerializeField] private Upgrading[] _upgradings;

    private const string Run = "Run";
    private const string Idle = "Idle";
    private const string Fall = "Fall";
    private const string Kick = "Kick";
    private const string JumpFinished = "JumpFinished";
    private const string Upgrade = "Upgrade";

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

        foreach (Upgrading upgrading in _upgradings)
        {
            upgrading.Upgraded += OnUpgraded;
        }
    }

    private void OnDisable()
    {
        _playerMovement.Stopped -= OnStopped;
        _playerMovement.MovingStarted -= OnMovingStarted;
        _player.Won -= OnWon;
        _player.Died -= OnDied;
        _landing.Landed -= OnLanded;

        foreach (Upgrading upgrading in _upgradings)
        {
            upgrading.Upgraded -= OnUpgraded;
        }
    }

    private void OnUpgraded()
    {
        _animator.SetTrigger(Upgrade);
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
}