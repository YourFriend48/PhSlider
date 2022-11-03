using System.Collections;
using UnityEngine;
using System;

public class JumpMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dependencyOfProgressByTimeShare;
    [SerializeField] private AnimationCurve _height;

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

    private Vector3 GetNextPosition(Vector3 startPosition, Vector3 target, float timeShare)
    {
        float progress = _dependencyOfProgressByTimeShare.Evaluate(timeShare);
        Vector3 linearPosition = Vector3.Lerp(startPosition, target, progress);
        float height = _height.Evaluate(timeShare);
        return linearPosition + Vector3.up * height;
    }

    private IEnumerator MovingLerp(Vector3 target, float requireTime)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (transform.position != target)
        {
            time += Time.deltaTime;

            if (time > requireTime)
            {
                time = requireTime;
            }

            float timeShare = time / requireTime;
            transform.position = GetNextPosition(startPosition, target, timeShare);
            yield return null;
        }

        Completed?.Invoke();
    }
}
