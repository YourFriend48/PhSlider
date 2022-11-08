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

    private bool _isLanded;
    private Collider _collider;

    public event Action ButtonClicked;
    public event Action Striked;
    public event Action Died;
    public event Action Fell;
    public event Action Failed;
    public event Action Won;
    public event Action Landed;
    public event Action<Player> Collided;
    public event Action<int> PowerChanged;

    public Power Power => _power;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _power.Changed += OnPowerChanged;
    }

    private void OnDisable()
    {
        _power.Changed -= OnPowerChanged;
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

    public void Die()
    {
        _powerVisualizer.Disappear();
        _collider.enabled = false;
        Died?.Invoke();
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