using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerParticlesHandler : MonoBehaviour
{
    [SerializeField] private Transform[] _particles;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        DisableParticles();
    }

    private void OnEnable()
    {
        _player.Landed += PlayerOnLanded;
        _player.Died += PlayerOnDied;
    }

    private void OnDisable()
    {
        _player.Landed -= PlayerOnLanded;
        _player.Died -= PlayerOnDied;
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

    private void PlayerOnDied()
    {
        DisableParticles();
    }

    private void PlayerOnLanded()
    {
        EnableParticles();
    }
}