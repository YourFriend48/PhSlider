using System.Collections;
using UnityEngine;

public class Landing : MonoBehaviour
{
    [SerializeField] private float _startingPositionY = 23.9f;
    [SerializeField] private float _fallSpeed = 16f;
    [SerializeField] private Platform _platform;
    [SerializeField] private GameObject _landingMark;
    [SerializeField] private float _destroyMarkAfter = 1.35f;
    [SerializeField] private ParticleSystem _landingEffect;

    private bool _isLanded;
    private Vector3 _landingPosition;
    private GameObject _mark;

    private void Start()
    {
        Bounds platformBounds = _platform.GetComponent<Collider>().bounds;
        Vector3 platformCenter = platformBounds.center;

        transform.position = new Vector3(platformCenter.x, _startingPositionY, platformCenter.z);
        _landingPosition = platformCenter + new Vector3(0, platformBounds.extents.y, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (_isLanded || collision.TryGetComponent(out Platform _) == false)
        {
            return;
        }

        _isLanded = true;

        _mark = Instantiate(_landingMark, transform.position, _landingMark.transform.rotation);
        Instantiate(_landingEffect, transform.position, _landingEffect.transform.rotation);

        StartCoroutine(DestroyMark());
    }

    private void Update()
    {
        if (_isLanded)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _landingPosition, _fallSpeed * Time.deltaTime);
    }

    private IEnumerator DestroyMark()
    {
        yield return new WaitForSeconds(_destroyMarkAfter);
        Destroy(_mark.gameObject);
    }
}