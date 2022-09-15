using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(PlayerAnimator))]
public class PlayerMovement : MonoBehaviour
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

    public void Move(Vector3 direction)
    {
        SetNextMovementPlatform(direction);
    }

    public void MoveBack()
    {
        SetNextMovementPlatform(Vector3.right);
    }

    public void MoveForward()
    {
        SetNextMovementPlatform(Vector3.left);
    }

    public void MoveLeft()
    {
        SetNextMovementPlatform(Vector3.back);
    }

    public void MoveRight()
    {
        SetNextMovementPlatform(Vector3.forward);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _player.Landed += LandingOnLanded;
        _player.Died += PlayerOnDied;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform) == false || platform != _nextPlatform)
        {
            return;
        }

        _playerAnimator.RunFlightToIdle();

        if (platform.TryGetComponent(out FinishPlatform _) == false)
        {
            return;
        }

        _isMovementEnabled = false;

        FinishReached?.Invoke();
        _playerAnimator.RunIdleToVictory();
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
        _player.Landed -= LandingOnLanded;
        _player.Died -= PlayerOnDied;
    }

    private IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(_delayAfterLanding);
        _isMovementEnabled = true;
        MovementEnabled?.Invoke();
    }

    private void LandingOnLanded()
    {
        StartCoroutine(EnableMovement());
    }

    private void PlayerOnDied()
    {
        _isMovementEnabled = false;
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