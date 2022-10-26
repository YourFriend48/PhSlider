using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private Transform _boxingGlove;
    [SerializeField] private PowerCanvas _powerCanvas;
    [SerializeField] private Material _deathMaterial;
    [SerializeField] private float _reboundForce = 3f;

    private bool _died;
    private Vector3 _gloveInitalScale;
    private bool _isLanded;
    private Rigidbody _rigidbody;

    public event UnityAction Died;
    public event UnityAction Landed;

    public void Die(Power killerPower)
    {
        if (_died)
        {
            return;
        }

        StartCoroutine(ScaleTo(_gloveInitalScale));
        _died = true;
        Died?.Invoke();
        GetComponentInChildren<SkinnedMeshRenderer>().material = _deathMaterial;

        Destroy(_powerCanvas.gameObject);

        killerPower.Increase();

        DisableHitAreas();
        ApplyRebound();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gloveInitalScale = _boxingGlove.transform.localScale;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isLanded || collision.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _isLanded = true;
        //_rigidbody.isKinematic = true;

        Landed?.Invoke();
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

    private void ApplyRebound()
    {
        Vector3 rebound = ((transform.rotation * Vector3.back) + Vector3.up) * _reboundForce;
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody currentRigidbody in rigidbodies)
        {
            currentRigidbody.velocity = rebound;
        }
    }

    private void DisableHitAreas()
    {
        HitArea[] hitAreas = GetComponentsInChildren<HitArea>();

        if (hitAreas.Length < 1)
        {
            return;
        }

        foreach (HitArea hitArea in hitAreas)
        {
            if (hitArea.TryGetComponent(out Collider hitAreaCollider))
            {
                hitAreaCollider.enabled = false;
            }
        }
    }
}