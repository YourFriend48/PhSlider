using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorablePlatformPart : MonoBehaviour
{
    [SerializeField] private Material _activatedMaterial;

    private bool _materialChanged;

    private void OnTriggerEnter(Collider collision)
    {
        if (_materialChanged || collision.TryGetComponent(out Player _) == false)
        {
            return;
        }

        _materialChanged = true;
        GetComponent<MeshRenderer>().material = _activatedMaterial;
    }
}