using System.Collections;
using UnityEngine;

public class CoverController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _gameCover;
    [SerializeField] private float _enableGameAfter = 1.13f;
    [SerializeField] private Transform _finishCover;
    [SerializeField] private float _enableFinishAfter = 0.805f;
    [SerializeField] private Transform _confeti;

    private void OnEnable()
    {
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
    }

    private void Start()
    {
        _gameCover.gameObject.SetActive(false);
        _finishCover.gameObject.SetActive(false);
        StartCoroutine(EnableGameCover());
    }

    private void OnDisable()
    {
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
    }

    private IEnumerator EnableFinishCover()
    {
        yield return new WaitForSeconds(_enableFinishAfter);
        _gameCover.gameObject.SetActive(false);
        _finishCover.gameObject.SetActive(true);
        _confeti.gameObject.SetActive(true);
    }

    private IEnumerator EnableGameCover()
    {
        yield return new WaitForSeconds(_enableGameAfter);
        _gameCover.gameObject.SetActive(true);
    }

    private void PlayerMovementOnFinishReached()
    {
        StartCoroutine(EnableFinishCover());
    }
}