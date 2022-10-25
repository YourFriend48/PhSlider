using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(RectTransform))]
public class RectMover : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dependencyOfProgressByTimeShare;

    private RectTransform _rectTransform;
    private Coroutine _moving;

    public event Action Completed;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void MoveTo(Vector2 target, float requireTime)
    {
        if (_moving != null)
        {
            StopCoroutine(_moving);
        }

        _moving = StartCoroutine(MovingTo(target, requireTime));
    }

    private IEnumerator MovingTo(Vector2 target, float requireTime)
    {
        float time = 0;
        float progress;
        Vector2 startPosition = _rectTransform.anchoredPosition;

        while (time != requireTime)
        {
            time += Time.deltaTime;

            if (time > requireTime)
            {
                time = requireTime;
            }

            progress = _dependencyOfProgressByTimeShare.Evaluate(time / requireTime);
            _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, target, progress);
            yield return null;
        }

        Completed?.Invoke();
    }
}
