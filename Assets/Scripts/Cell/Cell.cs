using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Cell : MonoBehaviour
{
    [SerializeField] private WalkableType _walkableType;

    private Gem _gem;
    private CellView _emptyTemplate;
    private CellView _wallTemplate;
    private CellView _roadTemplate;

    private CellView _cellView;

    public WalkableType Type => _walkableType;
    public CellView CellView => _cellView;

    public void Init(CellView emptyTemplate, CellView wallTemplate, CellView roadTemplate, Gem gem)
    {
        _emptyTemplate = emptyTemplate;
        _wallTemplate = wallTemplate;
        _roadTemplate = roadTemplate;
        _gem = gem;

        _walkableType = WalkableType.Unwalkable;
        SetWall();
    }

    private T InstiateAsPrefb<T>(T template) where T : MonoBehaviour
    {
        T instance = Instantiate(template, transform);
        return PrefabUtility.ConnectGameObjectToPrefab(instance.gameObject, template.gameObject).GetComponent<T>();
    }

    [ContextMenu("SetWall")]
    private void SetWall()
    {
        TryDestroyPreviousView();
        _cellView = InstiateAsPrefb(_wallTemplate);
    }

    [ContextMenu("SetEmpty")]
    private void SetEmpty()
    {
        TryDestroyPreviousView();
        _cellView = InstiateAsPrefb(_emptyTemplate);
    }

    [ContextMenu("SetRoad")]
    private void SetRoad()
    {
        TryDestroyPreviousView();
        _cellView = InstiateAsPrefb(_roadTemplate);
    }

    [ContextMenu("CreateGem")]
    private void CreateGem()
    {
        InstiateAsPrefb(_gem);
    }

    private void TryDestroyPreviousView()
    {
        if (_cellView != null)
        {
            DestroyImmediate(_cellView.gameObject);
        }
    }

    public enum WalkableType
    {
        Walkable,
        Unwalkable
    }

    public enum ViewType
    {
        Wall,
        Empty,
        Road
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.7f);
    }
}
