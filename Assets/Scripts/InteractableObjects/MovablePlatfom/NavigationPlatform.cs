using UnityEngine;

public class NavigationPlatform : MonoBehaviour
{
    public bool IsMovable;

    [SerializeField] private Transform _center;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    public Vector3 Center => _center.position;
}
