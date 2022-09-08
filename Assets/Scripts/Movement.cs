using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(PlayerAnimator))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _delayAfterLanding = 1.35f;
    [SerializeField] private float _speed = 19f;

    private bool _isMovementEnabled;
    private Platform _nextPlatform;
    private Vector3 _nextPosition;
    private Player _player;
    private PlayerAnimator _playerAnimator;

    public event UnityAction FinishReached;
    public event UnityAction LastHitInitiated;
    public event UnityAction MovementEnabled;

    private bool CanMove => _nextPosition != default && transform.position != _nextPosition;

    public void MoveBack()
    {
        Move(90f);
    }

    public void MoveForward()
    {
        Move(-90f);
    }

    public void MoveLeft()
    {
        Move(180f);
    }

    public void MoveRight()
    {
        Move(0f);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _player.Landed += Landing_OnLanded;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform) && platform == _nextPlatform)
        {
            _playerAnimator.RunFlightToIdle();

            if (platform.TryGetComponent(out FinishPlatform _))
            {
                FinishReached?.Invoke();
            }
        }
    }

    private void Update()
    {
        if (CanMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speed * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        _player.Landed -= Landing_OnLanded;
    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(_delayAfterLanding);
        _isMovementEnabled = true;
        MovementEnabled?.Invoke();
    }

    private void Landing_OnLanded()
    {
        StartCoroutine(EnableMovement());
    }

    private void Move(float degrees)
    {
        if (_isMovementEnabled == false || CanMove)
        {
            return;
        }

        transform.localEulerAngles = new Vector3(0, degrees, 0);

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
        const float RaycastDepth = .5f;
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

        for (var i = 0; i < hits; i++)
        {
            RaycastHit raycastHit = raycastHits[i];

            if (raycastHit.collider.TryGetComponent(out Platform currentPlatform))
            {
                requiredPlatform = currentPlatform;
                continue;
            }

            if (raycastHit.collider.TryGetComponent(out Roof _))
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