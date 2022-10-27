using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPlatform : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    public bool IsMovable { get; private set; }

    public Vector3 Center => _center.position;

    public void SetPlatformMovable()
    {
        IsMovable = true;
    }

    public void SetPlatformUnmovable()
    {
        IsMovable = false;
    }
}
