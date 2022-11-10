using System.Collections;
using UnityEngine;

public class MaterialXOffsetMover : MonoBehaviour
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
        float offsetX = _material.mainTextureOffset.x;
        float maxOffset = 1000000;

        while (true)
        {
            offsetX += _speed * Time.deltaTime;

            if (offsetX > maxOffset)
            {
                offsetX -= maxOffset;
            }

            _material.mainTextureOffset = new Vector2(offsetX, _material.mainTextureOffset.y);
            yield return null;
        }
    }
}
