using System;
using System.Collections;
using UnityEngine;

public class CoverHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Player _player;

    [SerializeField] private GameCover _gameCover;
    [SerializeField] private WinCover _winCover;
    [SerializeField] private LoseCover _loseCover;

    [SerializeField] private float _winPanelDelay;
    [SerializeField] private float _losePanelKickDelay;
    [SerializeField] private float _losePanelFallDelay;

    private void OnEnable()
    {
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
        _player.Died += PlayerOnDied;
        _player.Failed += PlayerOnFailed;
        _winCover.Opened += EndGameCoverOnOpened;
        _loseCover.Opened += EndGameCoverOnOpened;
    }

    private void Start()
    {
        _gameCover.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _winCover.Opened -= EndGameCoverOnOpened;
        _loseCover.Opened -= EndGameCoverOnOpened;
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
        _player.Died -= PlayerOnDied;
        _player.Failed -= PlayerOnDied;
    }

    private void PlayerMovementOnFinishReached()
    {
        StartCoroutine(DelayActivator(_winCover.gameObject, _winPanelDelay));
        //_winCover.gameObject.SetActive(true);
    }

    private IEnumerator DelayActivator(GameObject screen, float delay)
    {
        yield return new WaitForSeconds(delay);
        screen.gameObject.SetActive(true);
    }

    private void PlayerOnDied()
    {
        StartCoroutine(DelayActivator(_loseCover.gameObject, _losePanelKickDelay));
        //_loseCover.gameObject.SetActive(true);
    }

    private void PlayerOnFailed()
    {
        StartCoroutine(DelayActivator(_loseCover.gameObject, _losePanelFallDelay));
        //_loseCover.gameObject.SetActive(true);
    }

    private void EndGameCoverOnOpened()
    {
        _gameCover.Close();
    }
}