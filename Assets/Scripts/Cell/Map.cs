using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Map : MonoBehaviour
{
    [SerializeField, Min(1)] private int _length;
    [SerializeField, Min(1)] private int _width;
    [SerializeField] private Cell _template;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2Int _startPlayerPosition;
    [SerializeField] private Cell[] _lol;

    [Header("Templates")]
    [SerializeField] private CellView _roadTemplate;
    [SerializeField] private CellView _emptyTemplate;
    [SerializeField] private CellView _wallTemplate;

    private Cell[,] _map;
    private Vector2Int _playerPosition;

    public Vector2Int PlayerPosition => _playerPosition;

    public void Init()
    {
        _playerPosition = _startPlayerPosition;

        _map = new Cell[_length, _width];

        for (int i = 0; i < _length; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _map[i, j] = _lol[i * _width + j];
            }
        }
    }


    [ContextMenu("InstatiateMap")]
    public void InstatiateMap()
    {
        _lol = new Cell[_length * _width];

        for (int i = 0; i < _length; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                Vector3 position = new Vector3(i * _cellSize, 0, j * _cellSize);
                Cell cell = Instantiate(_template, transform);//, position, Quaternion.identity, transform);
                cell = PrefabUtility.ConnectGameObjectToPrefab(cell.gameObject, _template.gameObject).GetComponent<Cell>();
                cell.transform.position = position;
                cell.transform.rotation = Quaternion.identity;
                cell.Init(_emptyTemplate, _wallTemplate, _roadTemplate);
                _lol[i * _width + j] = cell;
            }
        }
    }

    public void SetPlayerPosition(Vector2Int position)
    {
        _playerPosition = position;
    }

    public Cell GetCell(Vector2Int coordinate)
    {
        return _map[coordinate.x, coordinate.y];
    }
}
