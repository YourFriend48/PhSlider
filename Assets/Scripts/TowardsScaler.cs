using System.Collections;
using UnityEngine;
using System;

public class TowardsScaler : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Coroutine _scaling;

    public event Action Completed;

    public void ScaleTowards(Vector3 target)
    {
        if (_scaling != null)
        {
            StopCoroutine(_scaling);
        }

        _scaling = StartCoroutine(ScalingTowards(target));
    }

    private IEnumerator ScalingTowards(Vector3 target)
    {
        while (transform.localScale != target)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, target, _speed * Time.deltaTime);
            yield return null;
        }

        Completed?.Invoke();
    }
}
