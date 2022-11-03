using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

public class GemExplosion : MonoBehaviour
{
    [SerializeField] private float _delayBeforeFly;
    [SerializeField] private int _count;
    [SerializeField] private Transform _center;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;
    [SerializeField] private GemPanel _gemPanel;

    [SerializeField] private float _minJumpTime;
    [SerializeField] private float _maxJumpTime;

    private WaitForSeconds _flyingWaiting;
    private List<MiniGem> _miniGems;

    private void Awake()
    {
        _flyingWaiting = new WaitForSeconds(_delayBeforeFly);
        _miniGems = new List<MiniGem>();
    }

    private void OnDisable()
    {
        foreach (MiniGem gem in _miniGems)
        {
            gem.JumpCompleted -= OnJumpCompleted;
            gem.FlyCompleted -= OnFlyingCompleted;
        }
    }

    private void Spawn()
    {
        MiniGem miniGem = PoolManager.Instance.Take<MiniGem>();
        miniGem.transform.position = transform.position;
        miniGem.gameObject.SetActive(true);
        _miniGems.Add(miniGem);

        Vector3 target = CalculateEndPoint();
        float time = Random.Range(_minJumpTime, _maxJumpTime);
        miniGem.JumpCompleted += OnJumpCompleted;
        miniGem.Jump(target, time);
    }

    public void Explode()
    {
        for (int i = 0; i < _count; i++)
        {
            Spawn();
        }
    }

    private void OnJumpCompleted(MiniGem miniGem)
    {
        StartCoroutine(DelayFlying(miniGem));
    }

    private IEnumerator DelayFlying(MiniGem miniGem)
    {
        yield return _flyingWaiting;
        miniGem.FlyCompleted += OnFlyingCompleted;
        miniGem.Fly(_gemPanel.transform.position);
    }

    private void OnFlyingCompleted(MiniGem miniGem)
    {
        Debug.Log("OnFlyingCompleted");
        miniGem.gameObject.SetActive(false);
    }

    private Vector3 CalculateEndPoint()
    {
        float radius = Random.Range(_minRadius, _maxRadius);
        float angle = Random.Range(0, 360);
        Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 target = quaternion * Vector3.forward * radius;
        return target;
    }
}
