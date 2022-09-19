using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _addToSizeY = 0.5f;
    [SerializeField] private float _sizeChangeSpeed = 1.8f;
    [SerializeField] private int _power;

    private Vector3 _finalPosition;
    private bool _grownUp;
    private bool _isActivated;
    private bool _isCycleComplete;
    private Vector3 _startingPosition;

    public int Power => _power;

    private void Start()
    {
        _startingPosition = transform.position;

        Vector3 finalPosition = _startingPosition;
        finalPosition.y += _addToSizeY;

        _finalPosition = finalPosition;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isActivated == false && collision.TryGetComponent(out Player _) == false)
        {
            return;
        }

        _isActivated = true;
    }

    private void Update()
    {
        if (_isActivated == false || _isCycleComplete)
        {
            return;
        }

        if (_grownUp == false && transform.position != _finalPosition)
        {
            transform.position = GetNewPosition(transform.position, _finalPosition);
            return;
        }

        _grownUp = true;

        if (transform.position != _startingPosition)
        {
            transform.position = GetNewPosition(transform.position, _startingPosition);
            return;
        }

        _isCycleComplete = true;
    }

    private Vector3 GetNewPosition(Vector3 current, Vector3 target)
    {
        return Vector3.MoveTowards(current, target, _sizeChangeSpeed * Time.deltaTime);
    }
}