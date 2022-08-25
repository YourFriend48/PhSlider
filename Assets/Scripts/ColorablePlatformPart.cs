using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorablePlatformPart : MonoBehaviour
{
    [SerializeField] private Material _activatedMaterial;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            GetComponent<MeshRenderer>().material = _activatedMaterial;
        }
    }
}