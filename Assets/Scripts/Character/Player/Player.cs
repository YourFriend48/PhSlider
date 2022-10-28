using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _ragdoll;

    [SerializeField] private Transform _boxingGlove;
    [SerializeField] private float _reboundForce = 3f;
    [SerializeField] private Rigidbody _rootBone;
    [SerializeField] private float _impactForce = 43f;
    [SerializeField] private ParticleSystem _boltsOfLighting;

    private bool _died;
    private Vector3 _gloveInitalScale;
    private bool _isLanded;
    private Collider _collider;

    public event UnityAction Died;
    public event UnityAction Landed;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _gloveInitalScale = _boxingGlove.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isLanded || other.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _isLanded = true;

        Landed?.Invoke();
    }

    public void Die()//(Power killerPower)
    {
        if (_died)
        {
            return;
        }

        _collider.enabled = false;
        StartCoroutine(ScaleTo(_gloveInitalScale));
        _died = true;
        Died?.Invoke();
        ChangeBodyToDead();
        TakeHit();

        //killerPower.Increase();
    }

    public void PlayBoltsOfLightingEffect()
    {
        _boltsOfLighting.Play();
    }

    private void ChangeBodyToDead()
    {
        _model.SetActive(false);
        _ragdoll.SetActive(true);
    }

    private void TakeHit()
    {
        Quaternion hitQuaternion = transform.rotation;

        Vector3 hitDirection = (hitQuaternion * Vector3.forward) + Vector3.up;
        _rootBone.AddForce(hitDirection * _impactForce, ForceMode.VelocityChange);
    }

    private IEnumerator ScaleTo(Vector3 target)
    {
        Vector3 startScale = _boxingGlove.transform.localScale;

        while (_boxingGlove.transform.localScale != _gloveInitalScale)
        {
            _boxingGlove.transform.localScale = Vector3.Lerp(startScale, target, _reboundForce * Time.deltaTime);
            yield return null;
        }
    }
}