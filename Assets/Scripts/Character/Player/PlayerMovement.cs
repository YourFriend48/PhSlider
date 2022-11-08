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
    [SerializeField] private float _depthOfFall;
    [SerializeField] private float _delayOfLose;

    private Player _player;
    private Coroutine _moving;
    private Vector3 _direction;
    private bool _isInputEnable = false;
    private bool _isMoving = false;

    public event Action FinishReached;
    public event Action LastHitInitiated;
    public event Action MovementEnabled;
    public event Action Completed;
    public event Action TurnEnded;
    public event Action Swipped;

    public event Action Stopped;
    public event Action MovingStarted;
    public event Action Won;
    public event Action Fell;

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
        Unscribe();
    }

    public void Move(Vector3 direction)
    {
        if (_isInputEnable)
        {
            OnSwipped(direction);
        }
    }

    private void Unscribe()
    {
        _isInputEnable = false;
        _player.Landed -= PlayerOnLanded;
        _player.Died -= PlayerOnDied;
        Completed -= OnCompleted;
        Completed -= OnFinishReached;
    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(_delayAfterLanding);
        MovementEnabled?.Invoke();
        _isInputEnable = true;
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
        Swipped?.Invoke();
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
                MoveAndMake(navigationPlatform.Center, StayOverAnAbyss);
                //Stop();
            }
            else
            {
                Move2(navigationPlatform.Center);
            }
        }
    }

    private void MoveAndMake(Vector3 target, Action nextAction)
    {
        if (_isInputEnable)
        {
            MovingStarted?.Invoke();
            _isInputEnable = false;
        }

        Completed += nextAction;
        MoveTo(target);
    }

    private void StayOverAnAbyss()
    {
        Fell?.Invoke();
    }

    public void Fall()
    {
        MoveAndMake(transform.position + _depthOfFall * Vector3.down, DoNothing);
        _player.Lose();
        //StartDelayLose();
    }
    private void DoNothing()
    {
    }

    private void StartDelayLose()
    {
        //_player.Lose();
        StartCoroutine(DelayLose());
    }

    private IEnumerator DelayLose()
    {
        yield return new WaitForSeconds(_delayOfLose);
        _player.Lose();
    }

    private void MoveToFinish(Vector3 target)
    {
        if (_isInputEnable)
        {
            MovingStarted?.Invoke();
            _isInputEnable = false;
        }

        Completed += OnFinishReached;
        MoveTo(target);
    }

    private void OnFinishReached()
    {
        Won?.Invoke();
        FinishReached?.Invoke();
        LastHitInitiated?.Invoke();
    }

    private void Move2(Vector3 target)
    {
        if (_isInputEnable)
        {
            MovingStarted?.Invoke();
            _isInputEnable = false;
        }

        Completed += OnCompleted;
        MoveTo(target);
    }

    private void Stop()
    {
        if (_isInputEnable == false)
        {
            Stopped?.Invoke();
        }

        _isInputEnable = true;

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
        Unscribe();

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