using UnityEngine;
using System;
using General;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ActiveBuildingArea[] _activeHouseAreas;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Enemy _bossEnemyPrefab;
    [SerializeField] private Material _enemyPlatform;
    [SerializeField] private Player _player;

    private void Start()
    {
        int firstEnemyPowerInLastLevel = 12;

        foreach (ActiveBuildingArea activeHouseArea in _activeHouseAreas)
        {
            if (activeHouseArea == null)
            {
                continue;
            }

            PlayerColorablePlatformPart[] colorableParts = activeHouseArea.GetComponentsInChildren<PlayerColorablePlatformPart>();

            foreach (PlayerColorablePlatformPart colorablePart in colorableParts)
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
                enemy.Init(_player);

                if (finishPlatform != null)
                {
                    finishPlatform.Init(enemy);
                }


                if (enemy.TryGetComponent(out Power power) == false)
                {
                    continue;
                }

                int additionalLevel = 0;

#if !UNITY_EDITOR
                additionalLevel = LevelLoader.Instance.Level / LevelLoader.Instance.Count * firstEnemyPowerInLastLevel;
#endif

                while (power.Current < platform.Power + additionalLevel)
                {
                    power.Increase();
                }
            }
        }
    }
}