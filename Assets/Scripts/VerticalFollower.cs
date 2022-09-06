using UnityEngine;

public class VerticalFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 4.5f;

    private void Update()
    {
        Vector3 targetPosition = _target.position;
        targetPosition.z = 0;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}