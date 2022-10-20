using System.Collections;
using UnityEngine;
using General;

[RequireComponent(typeof(Animator))]
public class LoseCover : Screen
{
    [SerializeField] private Player _player;
    [SerializeField] private float _enableAfterLose = 0.805f;

    private Animator _animator;

    private void OnEnable()
    {
        _player.Died += PlayerOnDied;
        OnEnableBase();
    }

    private void Start()
    {
        Close();

        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void OnDisable()
    {
        _player.Died -= PlayerOnDied;
        OnDisableBase();
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
}