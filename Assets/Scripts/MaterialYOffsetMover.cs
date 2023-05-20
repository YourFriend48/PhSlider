using System.Collections;
using UnityEngine;

public class MaterialYOffsetMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Material _material;

    private Coroutine _coroutine;

    public void SetMaterial(Material material)
    {
        _material = material;
    }

    public void Move()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Moving());
    }

    public void StopMove()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator Moving()
    {
        float offsetY = _material.mainTextureOffset.y;
        float maxOffset = 1000000;

        while (true)
        {
            offsetY += _speed * Time.deltaTime;

            if (offsetY > maxOffset)
            {
                offsetY -= maxOffset;
            }

            _material.mainTextureOffset = new Vector2(_material.mainTextureOffset.x, offsetY);
            yield return null;
        }
    }
}
