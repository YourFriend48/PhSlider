using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private float _reboundForce = 3f;
    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private ParticleSystem _boltsOfLighting;
    [SerializeField] private Power _power;
    [SerializeField] private PowerVisualizer _powerVisualizer;
    [SerializeField] private InvulnerabilityView _invulnerabilityView;
    [SerializeField] private MeshRenderer _invulnerabilityField;
    [SerializeField] private MaterialXOffsetMover _materialXOffsetMover;

    private bool _isLanded;
    private Collider _collider;
    //private DeathVariant _deathVariant;

    public event Action ButtonClicked;
    public event Action Striked;
    public event Action Died;
    public event Action Fell;
    public event Action Failed;
    public event Action Won;
    public event Action Landed;
    public event Action ReadyToFall;
    public event Action MadeInvulnerable;
    public event Action<Player> Collided;
    public event Action<int> PowerChanged;

    public Power Power => _power;
    public bool IsInvulnerable { get; private set; } = false;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _power.Changed += OnPowerChanged;
        _invulnerabilityView.BecameInvulnerable += OnBecameInvulnerable;
        _invulnerabilityView.Died += OnDie;
    }

    private void OnDisable()
    {
        _power.Changed -= OnPowerChanged;
        _invulnerabilityView.BecameInvulnerable -= OnBecameInvulnerable;
        _invulnerabilityView.Died -= OnDie;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isLanded || other.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _powerVisualizer.Appear();
        _isLanded = true;

        Landed?.Invoke();
    }

    public void ClickButton()
    {
        ButtonClicked?.Invoke();
    }

    private void OnPowerChanged(int value)
    {
        PowerChanged?.Invoke(value);
    }

    public void Lose()
    {
        Failed?.Invoke();
    }

    public void Fall()
    {
        Fell?.Invoke();
    }

    public void ReadyToDie(DeathVariant deathVariant)
    {
        //_deathVariant = deathVariant;
        //Изменить скорость игры
        _invulnerabilityView.Show(deathVariant);
    }

    public void OnBecameInvulnerable()
    {
        IsInvulnerable = true;
        MadeInvulnerable?.Invoke();
        _invulnerabilityField.gameObject.SetActive(true);
        _materialXOffsetMover.SetMaterial(_invulnerabilityField.material);
        _materialXOffsetMover.Move();
    }

    public void OnDie(DeathVariant deathVariant)
    {
        switch (deathVariant)
        {
            case DeathVariant.Fall:
                ReadyToFall?.Invoke();
                break;
            case DeathVariant.Laser:
                PlayBoltsOfLightingEffect();
                _powerVisualizer.Disappear();
                _collider.enabled = false;
                Died?.Invoke();
                break;
            case DeathVariant.Kick:
                _powerVisualizer.Disappear();
                _collider.enabled = false;
                Died?.Invoke();
                break;
        }
    }

    public void StrikeSound()
    {
        Striked?.Invoke();
    }

    public void Strike()
    {
        Collided?.Invoke(this);
    }

    public void Win()
    {
        _powerVisualizer.Disappear();
        Won?.Invoke();
    }

    public void PlayBoltsOfLightingEffect()
    {
        _boltsOfLighting.Play();
    }
}