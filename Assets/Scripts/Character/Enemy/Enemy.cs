using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Collider), typeof(Power), typeof(EnemyRotator))]
public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _ragdoll;

    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private float _destroySecAfterHide = 2f;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _fieldEffect;
    [SerializeField] private UIRotator _powerCanvas;
    [SerializeField] private Rigidbody _rootBone;
    [SerializeField] private bool _isBoss;

    [SerializeField] private Image _PowerCanvasField;
    [SerializeField] private Color _colorOfWeakEnemy;

    private EnemyRotator _enemyRotator;
    private Collider _collider;
    private Power _power;
    private Vector3 _center;
    private Player _player;

    public event Action Died;
    public event Action Struck;
    public event Action Collided;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _center = _collider.bounds.center;
        _power = GetComponent<Power>();
        _enemyRotator = GetComponent<EnemyRotator>();
    }

    private void OnDisable()
    {
        _player.Collided -= OnCollided;
        _player.Died -= Kick;
        _player.MadeInvulnerable -= OnMadeInvulnerable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _collider.enabled = false;
            Power playerPower = player.GetComponent<Power>();

            if (player.IsInvulnerable == false && playerPower.Current < _power.Current)
            {
                player.ReadyToDie(DeathVariant.Kick);
                _player.Died += Kick;
                _player.MadeInvulnerable += OnMadeInvulnerable;
            }
            else
            {
                Die();
            }
        }
    }

    private void Die()
    {
        _player.StrikeSound();

        if (_isBoss)
        {
            _player.Win();
            _player.Collided += OnCollided;
        }
        else
        {
            _enemyRotator.StopRotating();
            Vector3 hitEffectPosition = transform.position;
            Instantiate(_hitEffect, hitEffectPosition, Quaternion.identity);

            Died?.Invoke();

            Power playerPower = _player.GetComponent<Power>();
            playerPower.Increase();

            ChangeBodyToDead();

            TakeHit(_player);
        }
    }

    private void OnMadeInvulnerable()
    {
        Die();
    }

    private void Kick()
    {
        _player.Died -= Kick;
        Instantiate(_fieldEffect, _center, Quaternion.identity);
        Struck?.Invoke();
    }

    public void Init(Player player)
    {
        _player = player;
        _player.PowerChanged += OnPowerChanged;
        StartCoroutine(FakeInit());

        if (_isBoss == false)
        {
            _enemyRotator.Init(player);
            _enemyRotator.StartRotating();
        }
    }

    private IEnumerator FakeInit()
    {
        yield return null;
        OnPowerChanged(_player.Power.Current);
    }

    private void OnPowerChanged(int count)
    {
        if (_power.Current <= count)
        {
            _player.PowerChanged -= OnPowerChanged;
            _PowerCanvasField.color = _colorOfWeakEnemy;
        }
    }

    private void OnCollided(Player player)
    {
        player.Collided -= OnCollided;
        Collided?.Invoke();
        Vector3 hitEffectPosition = transform.position;
        Instantiate(_hitEffect, hitEffectPosition, Quaternion.identity);

        Died?.Invoke();

        //playerPower.Increase();

        ChangeBodyToDead();

        TakeHit(player);
    }

    private void ChangeBodyToDead()
    {
        _model.SetActive(false);
        _ragdoll.SetActive(true);
    }

    private void TakeHit(Player player)
    {
        Vector3 hitDirection = _rootBone.transform.position - player.transform.position;
        _rootBone.AddForce(hitDirection * _impactForce, ForceMode.VelocityChange);
    }
}