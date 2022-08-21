using System;
using UnityEngine;

[RequireComponent(typeof(Landing), typeof(PlayerAnimator))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 13f;

    private bool _canMove;
    private float _currentSpeed;

    private Landing _landing;
    private Platform _lastAffectedPlatform;
    private PlayerAnimator _playerAnimator;

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
        _landing = GetComponent<Landing>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _landing.ReadyToMove += Landing_OnReadyToMove;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform platform))
        {
            _lastAffectedPlatform = platform;
        }

        if (Math.Abs(_currentSpeed - _speed) > 0 || collision.TryGetComponent(out Roof _) == false)
        {
            return;
        }

        _currentSpeed = 0;
        _playerAnimator.RunFlightToIdle();

        PlaceInPlatformCenter();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _landing.ReadyToMove -= Landing_OnReadyToMove;
    }

    private bool IsWall()
    {
        var ray = new Ray(transform.position, transform.forward);
        float maxDistance = _lastAffectedPlatform.GetComponent<Collider>().bounds.size.x;

        return Physics.Raycast(ray, maxDistance);
    }

    private void Landing_OnReadyToMove()
    {
        _canMove = true;
    }

    private void Move(float degrees)
    {
        if (_canMove == false || _playerAnimator.IsIdle() == false)
        {
            return;
        }

        transform.localEulerAngles = new Vector3(0, degrees, 0);

        if (IsWall())
        {
            return;
        }

        _playerAnimator.RunIdleToFlight();
        _currentSpeed = _speed;
    }

    private void PlaceInPlatformCenter()
    {
        Vector3 platformCenter = _lastAffectedPlatform.GetComponent<Collider>().bounds.center;
        transform.position = new Vector3(platformCenter.x, transform.position.y, platformCenter.z);
    }
}