using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Lazer : MonoBehaviour
{
    [SerializeField] private TurnTimer _turnTimer;
    [SerializeField] private ParticleSystem _lightSignal;
    [SerializeField] private TowardsScaler _pivot;

    [Header("Ray")]
    [SerializeField] private Image _ray;
    [SerializeField] private MaterialYOffsetMover _materialYOffsetMover;

    [Header("Light")]
    [SerializeField] private IoIoScaler _light;
    [SerializeField] private Vector3 _lightScale = new Vector3(1, 1.1f, 1);

    private Collider _collider;
    private Vector3 _hideVector = new Vector3(0, 1, 1);

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _materialYOffsetMover.SetMaterial(_ray.material);
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
                _ray.gameObject.SetActive(true);
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
        _light.Completed += OnLightScaleCompleted;
        _light.Scale(_lightScale);
    }

    private void OnLightScaleCompleted()
    {
        _light.Scale(_lightScale);
    }

    private void Fade()
    {
        _pivot.Completed -= OnFadeCompleted;
        _pivot.Completed += OnFadeCompleted;
        _pivot.ScaleTowards(_hideVector);
    }

    private void OnFadeCompleted()
    {
        _light.Completed -= OnLightScaleCompleted;
        _pivot.Completed -= OnFadeCompleted;
        _materialYOffsetMover.StopMove();
    }
}
