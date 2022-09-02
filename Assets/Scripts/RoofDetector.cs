using UnityEngine;
using UnityEngine.Events;

public class RoofDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _positionIncrease = new Vector3(0, .2f, 0);

    public event UnityAction Faced;

    private void FixedUpdate()
    {
        Vector3 origin = transform.position + _positionIncrease;
        bool raycast = Physics.Raycast(
            origin,
            transform.TransformDirection(Vector3.forward),
            out RaycastHit raycastHit,
            Mathf.Infinity);

        if (raycast == false)
        {
            return;
        }

        if (raycastHit.collider.TryGetComponent(out Roof _))
        {
            Faced?.Invoke();
        }
    }
}