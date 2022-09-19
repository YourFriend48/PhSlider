using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class WinCover : Screen
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _enableAfterWin = 0.805f;

    private Animator _animator;

    private void OnEnable()
    {
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
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
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
        OnDisableBase();
    }

    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator EnableCover()
    {
        yield return new WaitForSeconds(_enableAfterWin);

        Open();
        _animator.enabled = true;
    }

    private void PlayerMovementOnFinishReached()
    {
        StartCoroutine(EnableCover());
    }
}