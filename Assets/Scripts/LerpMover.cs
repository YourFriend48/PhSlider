using System.Collections;
using UnityEngine;
using System;

public class LerpMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dependencyOfProgressByTimeShare;

    private Coroutine _moving;

    public event Action Completed;

    public void MoveLerp(Vector3 target, float requireTime)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingLerp(target, requireTime));
    }

    private IEnumerator MovingLerp(Vector3 target, float requireTime)
    {
        float time = 0;
        float progress;
        Vector3 startPosition = transform.position;

        while (transform.position != target)
        {
            time += Time.deltaTime;

            if (time > requireTime)
            {
                time = requireTime;
            }

            progress = _dependencyOfProgressByTimeShare.Evaluate(time / requireTime);
            transform.position = Vector3.Lerp(startPosition, target, progress);
            yield return null;
        }

        Completed?.Invoke();
    }
}
