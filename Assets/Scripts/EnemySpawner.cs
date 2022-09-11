using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ActiveBuildingArea[] _activeHouseAreas;
    [SerializeField] private Enemy _enemyPrefab;
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

                Vector3 platformCenter = colorablePartCollider.bounds.center;
                Instantiate(_enemyPrefab, platformCenter, _enemyPrefab.transform.rotation);
            }
        }
    }
}