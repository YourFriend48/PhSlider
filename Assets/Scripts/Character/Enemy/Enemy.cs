using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(EnemyAnimator), typeof(Power))]
public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _ragdoll;

    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private float _destroySecAfterHide = 2f;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _fieldEffect;
    [SerializeField] private PowerCanvas _powerCanvas;
    [SerializeField] private Rigidbody _rootBone;

    private EnemyAnimator _animator;
    private Collider _collider;
    private Power _power;
    private Vector3 _center;

    public event UnityAction Died;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _center = _collider.bounds.center;
        _power = GetComponent<Power>();
        _animator = GetComponent<EnemyAnimator>();
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

                _animator.RunIdleToVictory();
                player.Die();// (_power);
            }
            else
            {
                Vector3 hitEffectPosition = transform.position;
                Instantiate(_hitEffect, hitEffectPosition, Quaternion.identity);

                Died?.Invoke();

                playerPower.Increase();

                ChangeBodyToDead();

                TakeHit(player);
            }
        }
    }

    private void ChangeBodyToDead()
    {
        _model.SetActive(false);
        _ragdoll.SetActive(true);
    }

    private void TakeHit(Player player)
    {
        Quaternion hitQuaternion = player.transform.rotation;

        Vector3 hitDirection = (hitQuaternion * Vector3.forward) + Vector3.up;
        _rootBone.AddForce(hitDirection * _impactForce, ForceMode.VelocityChange);
    }
}