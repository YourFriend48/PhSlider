using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private float _hideSecAfterKill = 3f;
    [SerializeField] private Material _deathMaterial;
    [SerializeField] private ParticleSystem _hitEffect;
    
    private Collider[] _childrenColliders;
    private bool _died;

    public event UnityAction Died;

    private void Start()
    {
        _childrenColliders = GetComponentsInChildren<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out HitArea _) == false)
        {
            return;
        }

        IgnoreCollisionWith(collision.collider);

        if (_died)
        {
            return;
        }

        Instantiate(_hitEffect, collision.contacts[0].point, Quaternion.identity);
        _died = true;
        Died?.Invoke();

        ChangeBodyToDead();

        TakeHit(collision);

        StartCoroutine(HideBody());
    }

    private void ChangeBodyToDead()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = _deathMaterial;
        EnemyEmotion[] emotions = GetComponentsInChildren<EnemyEmotion>();

        foreach (EnemyEmotion emotion in emotions)
        {
            if (emotion.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.enabled = spriteRenderer.enabled == false;
            }
        }
    }

    private IEnumerator HideBody()
    {
        yield return new WaitForSeconds(_hideSecAfterKill);

        foreach (Collider currentCollider in _childrenColliders)
        {
            currentCollider.enabled = false;
        }
    }

    private void IgnoreCollisionWith(Collider ignoredCollider)
    {
        foreach (Collider currentCollider in _childrenColliders)
        {
            Physics.IgnoreCollision(currentCollider, ignoredCollider);
        }
    }

    private void TakeHit(Collision collision)
    {
        Quaternion hitQuaternion = collision.transform.rotation;
        var player = collision.gameObject.GetComponentInParent<Player>();

        if (player != null)
        {
            hitQuaternion = player.transform.rotation;
        }

        Vector3 hitDirection = (hitQuaternion * Vector3.forward) + Vector3.up;
        var hipsBone = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        hipsBone.AddForce(hitDirection * _impactForce, ForceMode.VelocityChange);
    }
}