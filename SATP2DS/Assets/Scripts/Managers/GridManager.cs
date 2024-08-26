using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

/// <summary>
/// GridManager class manages the grid system.
/// </summary>
public class GridManager : SingletonBehaviour<GridManager>
{
    public int width = 32;
    public int height = 32;
    
    public GameObject cellPrefab;
    public Transform gridParent;
    
    public GridCellControl[,] _gridCells;
    private IGridGenerator _gridGenerator;
    private AstarPathfinding _pathfinding;
    
    
    public void Initialize()
    {
        _gridGenerator = new GridGeneratorControl();
        InitializeGrid();
        InitializePathfinding();
    }

    void InitializeGrid()
    {
        _gridCells = _gridGenerator.GenerateGrid(width, height, cellPrefab, gridParent);
    }

    void InitializePathfinding()
    {
        _pathfinding = new AstarPathfinding(_gridCells);
    }
    
    public AstarPathfinding GetPathfinding()
    {
        return _pathfinding;
    }
    
    public Vector3 GetWorldPosition(int x, int y)
    { 
        return new Vector3(x, y, 0);
    }
    
    public Vector3Int GetGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y), 0);
    }
    
    public bool IsCellEmpty(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return _gridCells[x, y].IsEmpty;
        }
        return false;
    }
    
    
}