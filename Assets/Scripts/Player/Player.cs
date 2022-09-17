using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private Transform _boxingGlove;
    [SerializeField] private PowerCanvas _powerCanvas;
    [SerializeField] private Material _deathMaterial;
    [SerializeField] private EffectsObject _effectsObject;
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

        _died = true;
        Died?.Invoke();
        GetComponentInChildren<SkinnedMeshRenderer>().material = _deathMaterial;

        Destroy(_effectsObject.gameObject);
        Destroy(_powerCanvas.gameObject);

        killerPower.Increase();
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
        _rigidbody.isKinematic = true;

        Landed?.Invoke();
    }

    private void Update()
    {
        if (_died && _boxingGlove.transform.localScale != _gloveInitalScale)
        {
            _boxingGlove.transform.localScale = Vector3.Lerp(
                _boxingGlove.transform.localScale,
                _gloveInitalScale,
                _reboundForce * Time.deltaTime);
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
}