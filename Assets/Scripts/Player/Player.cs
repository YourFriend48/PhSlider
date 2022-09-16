using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Power))]
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
    private Power _power;
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

        _boxingGlove.transform.localScale = _gloveInitalScale;
        Destroy(_effectsObject.gameObject);
        Destroy(_powerCanvas.gameObject);

        killerPower.Add(_power.Current);
        ApplyRebound();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _power = GetComponent<Power>();

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