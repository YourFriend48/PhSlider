using UnityEngine;

public class CoverHandler : MonoBehaviour
{
    [SerializeField] private GameCover _gameCover;
    [SerializeField] private WinCover _winCover;
    [SerializeField] private LoseCover _loseCover;

    private void OnEnable()
    {
        _winCover.Opened += EndGameCoverOnOpened;
        _loseCover.Opened += EndGameCoverOnOpened;
    }

    private void Start()
    {
        _gameCover.gameObject.SetActive(true);
        _winCover.gameObject.SetActive(true);
        _loseCover.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _winCover.Opened -= EndGameCoverOnOpened;
        _loseCover.Opened -= EndGameCoverOnOpened;
    }

    private void EndGameCoverOnOpened()
    {
        _gameCover.Close();
    }
}