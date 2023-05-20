using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private float _addToSizeY = 0.5f;
    [SerializeField] private float _sizeChangeSpeed = 1.8f;
    [SerializeField] private int _power;

    private Vector3 _startingPosition;

    public int Power => _power;
    public Vector3 Center => _center.position;

    private void Start()
    {
        _startingPosition = transform.position;

        Vector3 finalPosition = _startingPosition;
        finalPosition.y += _addToSizeY;
    }
}