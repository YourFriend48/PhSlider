using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Power))]
public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private Material _deathMaterial;
    [SerializeField] private PowerCanvas _powerCanvas;
    
    private bool _died;
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

        Destroy(_powerCanvas.gameObject);

        killerPower.Add(_power.Current);

        GetComponentInChildren<SkinnedMeshRenderer>().material = _deathMaterial;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _power = GetComponent<Power>();
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
}