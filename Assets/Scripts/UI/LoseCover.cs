using System.Collections;
using UnityEngine;
using General;

[RequireComponent(typeof(Animator))]
public class LoseCover : Screen
{
    [SerializeField] private Player _player;
    [SerializeField] private float _enableAfterLose = 0.805f;
    [SerializeField] private GemLeaderboard _leaderboard;

    private Animator _animator;

    private void Start()
    {
        Close();

        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    protected override void OnButtonClick()
    {
        LevelLoader.Instance.Reload();
    }

    private IEnumerator EnableCover()
    {
        yield return new WaitForSeconds(_enableAfterLose);

        Open();
        _animator.enabled = true;
    }

    private void PlayerOnDied()
    {
        StartCoroutine(EnableCover());
    }

    protected override void Disable()
    {
        //_player.Died -= PlayerOnDied;
    }

    protected override void Enable()
    {
        StartCoroutine(EnableCover());
        //_player.Died += PlayerOnDied;
        _leaderboard.TryShow();
    }
}