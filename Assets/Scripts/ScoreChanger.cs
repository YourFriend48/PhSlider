using System.Collections;
using UnityEngine;
using System;

public class ScoreChanger : MonoBehaviour
{
    [SerializeField] private AnimationCurve _dependencyOfProgressByTimeShare;

    private int _previous;

    public event Action<int> Changed;

    public void ChangeScore(int startScore, int target, float time)
    {
        StartCoroutine(ChangingScore(startScore, target, time));
    }

    private IEnumerator ChangingScore(int startScore, int target, float requireTime)
    {
        float time = 0;
        float progress;
        _previous = startScore;

        while (time != requireTime)
        {
            time += Time.deltaTime;

            if (time > requireTime)
            {
                time = requireTime;
            }

            progress = _dependencyOfProgressByTimeShare.Evaluate(time / requireTime);

            int newScore = (int)Mathf.Lerp(startScore, target, progress);

            if (newScore != _previous)
            {
                Changed?.Invoke(newScore);
                _previous = newScore;
            }

            yield return null;
        }
    }
}
