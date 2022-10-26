using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorablePlatformPart : MonoBehaviour
{
    [SerializeField] private Material _activatedMaterial;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            GetComponent<MeshRenderer>().material = _activatedMaterial;
            _collider.enabled = false;
        }
    }
}