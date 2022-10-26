using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Lazer : MonoBehaviour
{
    //сделать таймер отдельным классом
    [SerializeField] private LazerTurret _lazerTurret;
    [SerializeField] private GameObject _lazer;
    [SerializeField] private ParticleSystem _lightSignal;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        OnTimeChanged(_lazerTurret.CurrentTime);
        _lazerTurret.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _lazerTurret.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(int value)
    {
        switch (value)
        {
            case 0:
                _collider.enabled = true;
                _lazer.gameObject.SetActive(true);
                _lightSignal.Stop();
                break;
            case 1:
                _collider.enabled = false;
                _lazer.gameObject.SetActive(false);
                _lightSignal.Play();
                break;
            default:
                _collider.enabled = false;
                _lazer.gameObject.SetActive(false);
                _lightSignal.Stop();
                break;
        }
    }
}
