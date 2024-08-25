using UnityEngine;

public class GridGenerator : IGridGenerator
{
    public GridCellController[,] GenerateGrid(int width, int height, GameObject cellPrefab, Transform parent)
    {
        GridCellController[,] gridCells = new GridCellController[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject cellObject = Object.Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity, parent);
                cellObject.name =$"Cell {x},{y}";
                cellObject.transform.position = new Vector2(x, y);
                cellObject.transform.parent = parent;
                gridCells[x, y] = new GridCellController(cellObject);
            }
        }

        return gridCells;
    }
}