using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

#pragma warning disable 0649
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Transform _groundTransform;
#pragma warning disable 0649

    private int _cellSize = 10;

    private Dictionary<Vector2Int, GameObject> _objectsDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _objectsDict = new Dictionary<Vector2Int, GameObject>();
            SetGroundScale();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetGroundScale()
    {
        _groundTransform.localScale = new Vector3(_gridSize.x, 1.0f, _gridSize.y);
    }

    public void SetGridSize(Vector2Int newSize)
    {
        _gridSize = newSize;
        SetGroundScale();
    }

    #region Position Management

    public Vector3 GetSnappedPosition(Vector3 position)
    {
        Vector2Int gridPos = GetPositionOnGrid(position);
        Vector3 newPos = new Vector3((gridPos.x + 0.5f) * _cellSize, transform.position.y, (gridPos.y + 0.5f) * _cellSize);
        return newPos;
    }

    // TODO: Manage out of bounds
    public Vector2Int GetPositionOnGrid(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x/_cellSize), Mathf.FloorToInt(position.z/_cellSize));
    }

    public bool IsPositionOnGrid(Vector2Int position)
    {
        return false;
    }
    #endregion

    #region Manage Grid Elements
    public bool CanAddElementAtPos(Vector2Int pos)
    {
        return !_objectsDict.ContainsKey(pos);
    }

    public void AddElementAtPos(Vector2Int pos, GameObject element)
    {
        if(CanAddElementAtPos(pos))
        {
            _objectsDict.Add(pos, element);
        }
    }

    public GameObject GetElementAtPos(Vector2Int pos)
    {
        _objectsDict.TryGetValue(pos, out GameObject res);
        return res;
    }
    #endregion
}
