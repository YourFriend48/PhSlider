using System.Collections.Generic;
using UnityEngine;

public class HitMarkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _hitMark;
    [SerializeField] private int _minPassedPlatforms = 4;
    [SerializeField] private Vector3 _shiftPosition = new Vector3(0, 0.308f, 0);
    [SerializeField] private float _roofInspectionDistance = 1f;

    private readonly List<Vector3> _markSpawnPoints = new List<Vector3>();

    private int _currentPlatformCount;

    private void FixedUpdate()
    {
        bool raycast = Physics.Raycast(
            transform.position,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit raycastHit,
            _roofInspectionDistance);

        if (raycast == false || raycastHit.collider.TryGetComponent(out InactiveRoof _) == false)
        {
            return;
        }

        Vector3 position = raycastHit.point + _shiftPosition;

        if (_currentPlatformCount >= _minPassedPlatforms && _markSpawnPoints.Contains(position) == false)
        {
            Instantiate(_hitMark, position, transform.rotation);
            _markSpawnPoints.Add(position);
        }

        _currentPlatformCount = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Platform _))
        {
            _currentPlatformCount++;
        }
    }
}