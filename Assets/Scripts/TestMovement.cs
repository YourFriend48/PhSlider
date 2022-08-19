using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Landing))]
public class TestMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool _canMove;

    private Landing _landing;
    private Platform _lastAffectedPlatform;

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

    public void Stop()
    {
        _speed = 0;
    }

    private void Awake()
    {
        _landing = GetComponent<Landing>();
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
            Debug.Log(_lastAffectedPlatform.name);
        }

        if (_canMove == false || collision.TryGetComponent(out Roof _) == false)
        {
            return;
        }

        _canMove = false;
        PlaceInCenter();
    }

    private void Update()
    {
        if (_canMove == false)
        {
            return;
        }

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _landing.ReadyToMove -= Landing_OnReadyToMove;
    }

    private void Landing_OnReadyToMove()
    {
        _canMove = true;
        MoveRight();
    }

    private void Move(float y)
    {
        transform.rotation = new Quaternion(0f, y, 0f, 0f);
    }

    private void PlaceInCenter()
    {
        if (_lastAffectedPlatform == null)
        {
            return;
        }

        Vector3 platformCenter;

        if (_lastAffectedPlatform.TryGetComponent(out Renderer platformRenderer))
        {
            platformCenter = platformRenderer.bounds.center;
        }
        else
        {
            var sumVector = new Vector3(0f, 0f, 0f);

            sumVector = _lastAffectedPlatform.transform.Cast<Transform>().Aggregate(
                sumVector,
                (current, child) => current + child.position);

            platformCenter = sumVector / _lastAffectedPlatform.transform.childCount;
        }

        transform.position = new Vector3(platformCenter.x, transform.position.y, platformCenter.z);
    }
}