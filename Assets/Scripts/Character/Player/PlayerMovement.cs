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

    private bool _isMovementEnabled;
    private Platform _nextPlatform;
    private Vector3 _nextPosition;
    private Player _player;
    private Coroutine _moving;
    private Vector3 _direction;
    private bool _isInputEnable = true;

    public event Action FinishReached;
    public event Action LastHitInitiated;
    public event Action MovementEnabled;
    public event Action Completed;

    private bool CanMove => _nextPosition != default && transform.position != _nextPosition;


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
        _isMovementEnabled = true;
        MovementEnabled?.Invoke();
    }

    private void PlayerOnLanded()
    {
        StartCoroutine(EnableMovement());
    }

    private void PlayerOnDied()
    {
        _isMovementEnabled = false;
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
            Move2(platform.Center);
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
    }

    private void OnCompleted()
    {
        Completed -= OnCompleted;
        OnSwipped(_direction);
    }

    private void MoveTo(Vector3 target)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingTo(target));
    }

    public void Win()
    {
        //if (platform.TryGetComponent(out FinishPlatform _) == false)
        //{
        //    return;
        //}

        FinishReached?.Invoke();
        _playerAnimator.RunIdleToVictory();
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

    private void SetNextMovementPlatform(Vector3 direction)
    {
        if (_isMovementEnabled == false || CanMove)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(direction);

        if (TryGetNextPlatform(out Platform nextPlatform) == false)
        {
            return;
        }

        _playerAnimator.RunIdleToFlight();

        Vector3 platformCenter = nextPlatform.GetComponent<Collider>().bounds.center;
        _nextPosition = new Vector3(platformCenter.x, transform.position.y, platformCenter.z);

        _nextPlatform = nextPlatform;

        if (nextPlatform.TryGetComponent(out FinishPlatform _))
        {
            LastHitInitiated?.Invoke();
        }
    }

    private bool TryGetNextPlatform(out Platform platform)
    {
        const float RaycastDepth = 0.5f;
        const int RaycastHitBuffer = 128;

        platform = null;

        Vector3 position = transform.position;
        position.y -= RaycastDepth;

        var raycastAllHits = new RaycastHit[RaycastHitBuffer];
        int hits = Physics.RaycastNonAlloc(position, transform.forward, raycastAllHits, Mathf.Infinity);

        if (hits < 1)
        {
            return false;
        }

        var raycastHits = new RaycastHit[hits];
        Array.Copy(raycastAllHits, raycastHits, hits);
        Array.Sort(raycastHits, (x, y) => x.distance.CompareTo(y.distance));

        Platform requiredPlatform = null;

        for (int i = 0; i < hits; i++)
        {
            RaycastHit raycastHit = raycastHits[i];

            if (raycastHit.collider.TryGetComponent(out Platform currentPlatform))
            {
                requiredPlatform = currentPlatform;
                continue;
            }

            if (raycastHit.collider.TryGetComponent(out InactiveRoof _))
            {
                break;
            }
        }

        if (requiredPlatform == null)
        {
            return false;
        }

        platform = requiredPlatform;

        return true;
    }
}