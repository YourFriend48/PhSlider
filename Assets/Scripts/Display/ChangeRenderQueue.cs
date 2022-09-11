using UnityEngine;

public class ChangeRenderQueue : MonoBehaviour
{
    [SerializeField] private int _renderQueue;
    [SerializeField] private Material _material;

    private void Start()
    {
        _material.renderQueue = _renderQueue;
    }
}