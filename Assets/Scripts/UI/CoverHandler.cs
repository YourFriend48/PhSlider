using System.Collections;
using UnityEngine;

public class CoverHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _gameCover;
    [SerializeField] private float _enableGameAfter = 1.13f;
    [SerializeField] private Transform _winCover;
    [SerializeField] private Transform _loseCover;
    [SerializeField] private float _enableEndGameAfter = 0.805f;
    [SerializeField] private Transform _confeti;

    private enum EndGameState
    {
        Win,
        Lose
    }

    private void OnEnable()
    {
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
        _player.Died += PlayerOnDied;
    }

    private void Start()
    {
        _gameCover.gameObject.SetActive(false);
        _winCover.gameObject.SetActive(false);
        _loseCover.gameObject.SetActive(false);
        StartCoroutine(EnableGameCover());
    }

    private void OnDisable()
    {
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
        _player.Died -= PlayerOnDied;
    }

    private IEnumerator EnableEndGameCover(EndGameState endGameState)
    {
        yield return new WaitForSeconds(_enableEndGameAfter);
        _gameCover.gameObject.SetActive(false);

        if (endGameState == EndGameState.Lose)
        {
            _loseCover.gameObject.SetActive(true);
        }
        else
        {
            _winCover.gameObject.SetActive(true);
            _confeti.gameObject.SetActive(true);
        }
    }

    private IEnumerator EnableGameCover()
    {
        yield return new WaitForSeconds(_enableGameAfter);
        _gameCover.gameObject.SetActive(true);
    }

    private void PlayerMovementOnFinishReached()
    {
        StartCoroutine(EnableEndGameCover(EndGameState.Win));
    }

    private void PlayerOnDied()
    {
        StartCoroutine(EnableEndGameCover(EndGameState.Lose));
    }
}