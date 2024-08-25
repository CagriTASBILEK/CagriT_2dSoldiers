using UnityEngine;

public interface IGridGenerator
{
    GridCellController[,] GenerateGrid(int width, int height, GameObject cellPrefab, Transform parent);
}