using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Lazer : MonoBehaviour
{
    [SerializeField] private TurnTimer _turnTimer;
    [SerializeField] private GameObject _lazer;
    [SerializeField] private ParticleSystem _lightSignal;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        OnTimeChanged(_turnTimer.CurrentTime);
        _turnTimer.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _turnTimer.TimeChanged -= OnTimeChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Die();
        }
    }

    private void OnTimeChanged(int value)
    {
        switch (value)
        {
            case 0:
                _collider.enabled = true;
                _lazer.gameObject.SetActive(true);
                _lightSignal.Stop();
                break;
            case 1:
                _collider.enabled = false;
                _lazer.gameObject.SetActive(false);
                _lightSignal.Play();
                break;
            default:
                _collider.enabled = false;
                _lazer.gameObject.SetActive(false);
                _lightSignal.Stop();
                break;
        }
    }
}
