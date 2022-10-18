using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Cell : MonoBehaviour
{
    [SerializeField] private WalkableType _walkableType;

    private CellView _emptyTemplate;
    private CellView _wallTemplate;
    private CellView _roadTemplate;

    private CellView _cellView;

    public WalkableType Type => _walkableType;
    public CellView CellView => _cellView;

    public void Init(CellView emptyTemplate, CellView wallTemplate, CellView roadTemplate)
    {
        _emptyTemplate = emptyTemplate;
        _wallTemplate = wallTemplate;
        _roadTemplate = roadTemplate;

        _walkableType = WalkableType.Unwalkable;
        SetWall();
    }

    private void InstiateAsPrefb(CellView template)
    {
        _cellView = Instantiate(template, transform);
        _cellView = PrefabUtility.ConnectGameObjectToPrefab(_cellView.gameObject, template.gameObject).GetComponent<CellView>();
    }

    [ContextMenu("SetWall")]
    private void SetWall()
    {
        TryDestroyPreviousView();
        InstiateAsPrefb(_wallTemplate);
    }

    [ContextMenu("SetEmpty")]
    private void SetEmpty()
    {
        TryDestroyPreviousView();
        InstiateAsPrefb(_emptyTemplate);
    }

    [ContextMenu("SetRoad")]
    private void SetRoad()
    {
        TryDestroyPreviousView();
        InstiateAsPrefb(_roadTemplate);
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
