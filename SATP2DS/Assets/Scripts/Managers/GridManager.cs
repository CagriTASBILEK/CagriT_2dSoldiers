using System;
using UnityEngine;

/// <summary>
/// GridManager class manages the grid system.
/// </summary>
public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    
    public int width = 32;
    public int height = 32;
    
    public GameObject cellPrefab;
    public Transform gridParent;
    
    public GridCellControl[,] _gridCells;
    private IGridGenerator _gridGenerator;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
       
        _gridGenerator = new GridGenerator();
        InitializeGrid();
    }

    void InitializeGrid()
    {
        _gridCells = _gridGenerator.GenerateGrid(width, height, cellPrefab, gridParent);
        
    }

    public Vector3 GetWorldPosition(int x, int y)
    { 
        return new Vector3(x, y, 0);
    }
}