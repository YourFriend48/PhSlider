using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _delayAfterLanding = 1.35f;
    [SerializeField] private float _speed = 17f;
    [SerializeField] private float _maxMovement = 1f;
    [SerializeField] private ColliderChecker _colliderChecker;
    [SerializeField] private GameObject _model;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private Player _player;
    private Coroutine _moving;
    private Vector3 _direction;
    private bool _isInputEnable = true;
    private bool _isMoving = false;

    public event Action FinishReached;
    public event Action LastHitInitiated;
    public event Action MovementEnabled;
    public event Action Completed;
    public event Action TurnEnded;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Landed += PlayerOnLanded;
        _player.Died += PlayerOnDied;
    }

    private void OnDisable()
    {
        _player.Landed -= PlayerOnLanded;
        _player.Died -= PlayerOnDied;
        Completed -= OnCompleted;
        Completed -= OnFinishReached;
    }

    public void Move(Vector3 direction)
    {
        if (_isInputEnable)
        {
            OnSwipped(direction);
        }
    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(_delayAfterLanding);
        MovementEnabled?.Invoke();
    }

    private void PlayerOnLanded()
    {
        StartCoroutine(EnableMovement());
    }

    private void PlayerOnDied()
    {
        StopMove();
        _speed = 0f;
    }

    private void OnSwipped(Vector3 direction)
    {
        _model.transform.rotation = Quaternion.LookRotation(direction);
        _direction = direction;
        Collider other = _colliderChecker.Check(direction);

        if (other == null)
        {
            throw new Exception($"Have not collision with objects on {direction} direction");
        }
        else if (other.GetComponent<InactiveRoof>())
        {
            Stop();
        }
        else if (other.TryGetComponent(out Platform platform))
        {
            if (platform is FinishPlatform)
            {
                MoveToFinish(platform.Center);
            }
            else
            {
                Move2(platform.Center);
            }
        }
        else if (other.TryGetComponent(out AppearingWall wall))
        {
            if (wall.IsRaised)
            {
                Stop();
            }
            else
            {
                Move2(wall.Center);
            }
        }
        else if (other.TryGetComponent(out NavigationPlatform navigationPlatform))
        {
            if (navigationPlatform.IsMovable == false)
            {
                Stop();
            }
            else
            {
                Move2(navigationPlatform.Center);
            }
        }
    }

    private void MoveToFinish(Vector3 target)
    {
        if (_isInputEnable)
        {
            _playerAnimator.RunIdleToFlight();
            _isInputEnable = false;
        }

        Completed += OnFinishReached;
        MoveTo(target);
    }

    private void OnFinishReached()
    {
        _playerAnimator.RunIdleToVictory();
        FinishReached?.Invoke();
        LastHitInitiated?.Invoke();
    }

    private void Move2(Vector3 target)
    {
        if (_isInputEnable)
        {
            _playerAnimator.RunIdleToFlight();
            _isInputEnable = false;
        }

        Completed += OnCompleted;
        MoveTo(target);
    }

    private void Stop()
    {
        _isInputEnable = true;
        _playerAnimator.RunFlightToIdle();

        if (_isMoving)
        {
            TurnEnded?.Invoke();
            _isMoving = false;
        }
    }

    private void OnCompleted()
    {
        Completed -= OnCompleted;
        OnSwipped(_direction);
    }

    private void MoveTo(Vector3 target)
    {
        _isMoving = true;

        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingTo(target));
    }

    public void StopMove()
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }
    }

    private IEnumerator MovingTo(Vector3 target)
    {
        while (transform.position != target)
        {
            //float speed = Mathf.Min(_speed * Time.deltaTime, _maxMovement);
            float speed = _speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            yield return null;
        }

        Completed?.Invoke();
    }
}