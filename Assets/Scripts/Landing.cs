using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Landing : MonoBehaviour
{
    [SerializeField] private float _startingPositionY = 23.9f;
    [SerializeField] private float _fallSpeed = 16f;
    [SerializeField] private Platform _platform;
    [SerializeField] private GameObject _landingMark;
    [SerializeField] private float _destroyMarkAfter = 1.35f;

    private bool _isLanded;
    private GameObject _mark;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Vector3 platformCenter = _platform.GetComponent<Collider>().bounds.center;
        transform.position = new Vector3(platformCenter.x, _startingPositionY, platformCenter.z);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isLanded || collision.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _mark = Instantiate(_landingMark, transform.position, _landingMark.transform.rotation);
        StartCoroutine(DestroyMark());

        _isLanded = true;
    }

    private void Update()
    {
        if (_isLanded)
        {
            return;
        }

        _rigidbody.velocity = Vector3.down * _fallSpeed;
    }

    private IEnumerator DestroyMark()
    {
        yield return new WaitForSeconds(_destroyMarkAfter);
        Destroy(_mark.gameObject);
    }
}