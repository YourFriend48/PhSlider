using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    [SerializeField] private float _distance = 1f;
    [SerializeField] private LayerMask _layerMask;

    public Collider Check(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, _distance, _layerMask, QueryTriggerInteraction.Collide))
        {
            return hitInfo.collider;
        }
        else
        {
            return null;
        }
    }
}