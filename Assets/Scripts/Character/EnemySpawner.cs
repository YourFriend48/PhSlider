using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ActiveBuildingArea[] _activeHouseAreas;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Enemy _bossEnemyPrefab;
    [SerializeField] private Material _enemyPlatform;

    private void Start()
    {
        foreach (ActiveBuildingArea activeHouseArea in _activeHouseAreas)
        {
            if (activeHouseArea == null)
            {
                continue;
            }

            ColorablePlatformPart[] colorableParts = activeHouseArea.GetComponentsInChildren<ColorablePlatformPart>();

            foreach (ColorablePlatformPart colorablePart in colorableParts)
            {
                if (colorablePart.TryGetComponent(out MeshRenderer meshRenderer) == false
                    || meshRenderer.sharedMaterial != _enemyPlatform
                    || colorablePart.TryGetComponent(out Collider colorablePartCollider) == false)
                {
                    continue;
                }

                var platform = colorablePartCollider.GetComponentInParent<Platform>();

                if (platform == null)
                {
                    continue;
                }

                Vector3 platformCenter = colorablePartCollider.bounds.center;
                var finishPlatform = colorablePartCollider.GetComponentInParent<FinishPlatform>();

                Enemy enemyPrefab = finishPlatform != null ? _bossEnemyPrefab : _enemyPrefab;
                Enemy enemy = Instantiate(enemyPrefab, platformCenter, _bossEnemyPrefab.transform.rotation);



                if (enemy.TryGetComponent(out Power power) == false)
                {
                    continue;
                }

                while (power.Current < platform.Power)
                {
                    power.Increase();
                }
            }
        }
    }
}