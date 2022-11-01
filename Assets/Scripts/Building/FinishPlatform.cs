using UnityEngine;

public class FinishPlatform : Platform
{
    [SerializeField] private TowardsScaler _towardsScaler;

    private Enemy _boss;

    public void Init(Enemy enemy)
    {
        _boss = enemy;
        _boss.Collided += OnCollided;
    }

    private void OnDisable()
    {
        if (_boss != null)
            _boss.Collided -= OnCollided;
    }

    private void OnCollided()
    {
        StartWave();
    }

    private void StartWave()
    {
        _towardsScaler.ScaleTowards(Vector3.one * 10f);
    }
}