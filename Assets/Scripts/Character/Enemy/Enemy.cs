using UnityEngine;
using System;

[RequireComponent(typeof(Collider), typeof(Power), typeof(EnemyRotator))]
public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _ragdoll;

    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private float _destroySecAfterHide = 2f;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _fieldEffect;
    [SerializeField] private CameraRotator _powerCanvas;
    [SerializeField] private Rigidbody _rootBone;
    [SerializeField] private bool _isBoss;

    private EnemyRotator _enemyRotator;
    private Collider _collider;
    private Power _power;
    private Vector3 _center;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _collider.enabled = false;
            Power playerPower = player.GetComponent<Power>();

            if (playerPower.Current < _power.Current)
            {
                Instantiate(_fieldEffect, _center, Quaternion.identity);
                Struck?.Invoke();
                player.Die();// (_power);
            }
            else
            {
                if (_isBoss)
                {
                    player.Win();
                    player.Collided += OnCollided;
                }
                else
                {
                    _enemyRotator.StopRotating();
                    Vector3 hitEffectPosition = transform.position;
                    Instantiate(_hitEffect, hitEffectPosition, Quaternion.identity);

                    Died?.Invoke();

                    playerPower.Increase();

                    ChangeBodyToDead();

                    TakeHit(player);
                }
            }
        }
    }

    public void Init(Player player)
    {
        if (_isBoss == false)
        {
            _enemyRotator.Init(player);
            _enemyRotator.StartRotating();
        }
    }

    private void OnCollided(Player player)
    {
        player.Collided -= OnCollided;//Unscribe does not garantee
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