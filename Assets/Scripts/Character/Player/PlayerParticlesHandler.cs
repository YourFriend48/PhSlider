using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerMovement))]
public class PlayerParticlesHandler : MonoBehaviour
{
    [SerializeField] private Transform[] _particles;

    private Player _player;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        DisableParticles();
    }

    private void OnEnable()
    {
        _player.Landed += PlayerOnLanded;
        _player.Died += PlayerOnDied;
        _playerMovement.FinishReached += PlayerMovementOnFinishReached;
    }

    private void OnDisable()
    {
        _player.Landed -= PlayerOnLanded;
        _player.Died -= PlayerOnDied;
        _playerMovement.FinishReached -= PlayerMovementOnFinishReached;
    }

    private void DisableParticles()
    {
        foreach (Transform particle in _particles)
        {
            particle.gameObject.SetActive(false);
        }
    }

    private void EnableParticles()
    {
        foreach (Transform particle in _particles)
        {
            particle.gameObject.SetActive(true);
        }
    }

    private void PlayerMovementOnFinishReached()
    {
        DisableParticles();
    }

    private void PlayerOnDied()
    {
        DisableParticles();
    }

    private void PlayerOnLanded()
    {
        EnableParticles();
    }
}