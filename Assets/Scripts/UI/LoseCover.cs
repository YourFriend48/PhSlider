using System.Collections;
using UnityEngine;
using General;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class LoseCover : EndScreen
{
    [SerializeField] private Player _player;
    [SerializeField] private float _enableAfterLose = 0.805f;
    [SerializeField] private GemLeaderboard _leaderboard;

    private Animator _animator;
    public static event Action<int> Failed;

    protected override void OnButtonClick()
    {
        Failed?.Invoke(SceneManager.GetActiveScene().buildIndex);
        LevelLoader.Instance.Reload();
    }

    private void Lose()
    {
        Open();
        _animator.enabled = true;
        //_leaderboard.TryShow();
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