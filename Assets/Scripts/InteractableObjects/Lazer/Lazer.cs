using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Lazer : MonoBehaviour
{
    [SerializeField] private TurnTimer _turnTimer;
    [SerializeField] private Image _lazer;
    [SerializeField] private ParticleSystem _lightSignal;
    [SerializeField] private TowardsScaler _pivot;
    [SerializeField] private MaterialYOffsetMover _materialYOffsetMover;

    private Collider _collider;
    private Vector3 _hideVector = new Vector3(0, 1, 1);

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _materialYOffsetMover.SetMaterial(_lazer.material);
    }

    private void OnEnable()
    {
        OnTimeChanged(_turnTimer.CurrentTime);
        _turnTimer.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _pivot.Completed -= OnFadeCompleted;
        _turnTimer.TimeChanged -= OnTimeChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Die();
            player.PlayBoltsOfLightingEffect();
        }
    }

    private void OnTimeChanged(int value)
    {
        switch (value)
        {
            case 0:
                _collider.enabled = true;
                Appear();
                _lazer.gameObject.SetActive(true);
                _lightSignal.Stop();
                break;
            case 1:
                _collider.enabled = false;
                Fade();
                _lightSignal.Play();
                break;
            default:
                _collider.enabled = false;
                Fade();
                _lightSignal.Stop();
                break;
        }
    }

    private void Appear()
    {
        _pivot.ScaleTowards(Vector3.one);
        _materialYOffsetMover.Move();
    }

    private void Fade()
    {
        _pivot.Completed -= OnFadeCompleted;
        _pivot.Completed += OnFadeCompleted;
        _pivot.ScaleTowards(_hideVector);
    }

    private void OnFadeCompleted()
    {
        _pivot.Completed -= OnFadeCompleted;
        _materialYOffsetMover.StopMove();
    }
}
