using UnityEngine;
/// <summary>
/// GridGenerator class implements the IGridGenerator interface
/// and is responsible for generating a grid of cells.
/// </summary>
public class GridGenerator : IGridGenerator
{
    /// <summary>
    /// Generates a grid of cells.
    /// </summary>
    /// <param name="width">The width of the grid.</param>
    /// <param name="height">The height of the grid.</param>
    /// <param name="cellPrefab">The prefab for the grid cell.</param>
    /// <param name="parent">The parent transform for the grid cells.</param>
    /// <returns>A 2D array of GridCellController representing the grid.</returns>
    public GridCellController[,] GenerateGrid(int width, int height, GameObject cellPrefab, Transform parent)
    {
        GridCellController[,] gridCells = new GridCellController[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject cellObject = Object.Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity, parent);
                cellObject.name = $"Cell {x},{y}";
                cellObject.transform.position = new Vector2(x, y);
                cellObject.transform.parent = parent;
                gridCells[x, y] = new GridCellController(cellObject);
            }
        }
        return gridCells;
    }
}