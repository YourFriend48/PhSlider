using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Finance;

[RequireComponent(typeof(Collider))]
public class Gem : MonoBehaviour
{
    [SerializeField, Min(1)] private int _value;
    [SerializeField] private Scaler _scaler;
    [SerializeField] private ParticleSystem _sparkles;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Collect();
        }
    }

    private void Collect()
    {
        _collider.enabled = false;
        WalletHolder.Instance.PutIn(_value);
        _scaler.Completed += OnScaleCompleted;
        _scaler.ScaleTo(Vector3.zero);
        _sparkles.Play();
    }

    private void OnScaleCompleted()
    {
        Destroy(this);
    }
}
