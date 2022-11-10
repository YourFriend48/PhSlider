using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("Sounds")]
    [SerializeField] private AudioClip _landing;
    [SerializeField] private AudioClip[] _strikes;
    [SerializeField] private AudioClip _scream;
    [SerializeField] private AudioClip _lose;
    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _button;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _player.Landed += OnLanded;
        _player.Striked += OnStriked;
        _player.Collided += OnCollided;
        _player.Died += OnDied;
        _player.Fell += OnDied;
        _player.Failed += OnFailed;
        _player.Won += OnWon;
        _player.ButtonClicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        _player.Landed -= OnLanded;
        _player.Striked -= OnStriked;
        _player.Collided -= OnCollided;
        _player.Died -= OnDied;
        _player.Fell -= OnDied;
        _player.Failed -= OnFailed;
        _player.Won -= OnWon;
        _player.ButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        AudioSource.PlayClipAtPoint(_button, _camera.transform.position);
    }

    private void OnWon()
    {
        AudioSource.PlayClipAtPoint(_win, _camera.transform.position, 4f);
    }

    private void OnFailed()
    {
        AudioSource.PlayClipAtPoint(_lose, _camera.transform.position);
    }

    private void OnDied()
    {
        AudioSource.PlayClipAtPoint(_scream, _camera.transform.position);
    }

    private void OnLanded()
    {
        AudioSource.PlayClipAtPoint(_landing, _camera.transform.position);
    }

    private void OnCollided(Player player)
    {
        OnStriked();
    }

    private void OnStriked()
    {
        AudioClip audioClip = GetRandomClip(_strikes);
        AudioSource.PlayClipAtPoint(audioClip, _camera.transform.position);
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
