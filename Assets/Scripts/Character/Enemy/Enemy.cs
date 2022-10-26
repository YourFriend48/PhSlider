using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(EnemyAnimator), typeof(Power))]
public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _enemyModel;
    [SerializeField] private GameObject _ragdoll;

    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private float _hideSecAfterKill = 3f;
    [SerializeField] private Material _deathMaterial;
    [SerializeField] private float _destroySecAfterHide = 2f;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _fieldEffect;
    [SerializeField] private PowerCanvas _powerCanvas;
    [SerializeField] private Rigidbody _rootBone;

    private EnemyAnimator _animator;
    private Collider[] _childrenColliders;
    private Collider _collider;
    private Power _power;
    private Vector3 _center;

    public event UnityAction Died;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _center = _collider.bounds.center;
    }

    private void Start()
    {
        _childrenColliders = GetComponentsInChildren<Collider>();
        _power = GetComponent<Power>();
        _animator = GetComponent<EnemyAnimator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Enter");
            _collider.enabled = false;
            Power playerPower = player.GetComponent<Power>();

            if (playerPower.Current < _power.Current)
            {
                //Bounds colliderBounds = _collider.bounds;
                Instantiate(_fieldEffect, _center, Quaternion.identity);

                _animator.RunIdleToVictory();
                player.Die(_power);
            }
            else
            {
                //Vector3 hitEffectPosition = collision.GetContact(collision.contactCount - 1).point;
                Vector3 hitEffectPosition = transform.position;
                Instantiate(_hitEffect, hitEffectPosition, Quaternion.identity);

                Died?.Invoke();

                //Destroy(_powerCanvas.gameObject);

                playerPower.Increase();

                ChangeBodyToDead();

                TakeHit(player);

                //StartCoroutine(HideBody());
                //StartCoroutine(DestroyBody());
            }
        }
    }

    private void ChangeBodyToDead()
    {
        _enemyModel.SetActive(false);
        _ragdoll.SetActive(true);
    }

    private IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(_destroySecAfterHide);
        Destroy(gameObject);
    }

    private void TakeHit(Player player)
    {
        Quaternion hitQuaternion = player.transform.rotation;

        Vector3 hitDirection = (hitQuaternion * Vector3.forward) + Vector3.up;
        _rootBone.AddForce(hitDirection * _impactForce, ForceMode.VelocityChange);
        _animator.enabled = false;
    }
}