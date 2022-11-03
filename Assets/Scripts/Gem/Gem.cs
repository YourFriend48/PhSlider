using UnityEngine;
using Finance;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(GemCost))]
[RequireComponent(typeof(Scaler))]
public class Gem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sparkles;
    [SerializeField] private GameObject _model;
    //[SerializeField] private GemExplosion _gemExplosion;

    private Scaler _scaler;
    private Collider _collider;
    private GemCost _cost;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _cost = GetComponent<GemCost>();
        _scaler = GetComponent<Scaler>();
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
        //_gemExplosion.Explode();
        WalletHolder.Instance.PutIn(_cost.Current);
        _scaler.Completed += OnScaleCompleted;
        _scaler.ScaleTo(Vector3.zero);
        _sparkles.Play();
    }

    private void OnScaleCompleted()
    {
        _model.SetActive(false);
        Destroy(this);
    }
}
