using System.Collections;
using UnityEngine;
using General;

[RequireComponent(typeof(Animator))]
public class LoseCover : EndScreen
{
    [SerializeField] private Player _player;
    [SerializeField] private float _enableAfterLose = 0.805f;
    [SerializeField] private GemLeaderboard _leaderboard;

    private Animator _animator;

    protected override void OnButtonClick()
    {
        LevelLoader.Instance.Reload();
    }

    private IEnumerator EnableCover()
    {
        yield return new WaitForSeconds(_enableAfterLose);

        Lose();
    }

    private void Lose()
    {
        Open();
        _animator.enabled = true;
        _leaderboard.TryShow();
    }

    protected override void Disable()
    {
    }

    protected override void Enable()
    {
        Lose();
        //StartCoroutine(EnableCover());
    }

    protected override void OnAwake()
    {
        _animator = GetComponent<Animator>();
    }
}