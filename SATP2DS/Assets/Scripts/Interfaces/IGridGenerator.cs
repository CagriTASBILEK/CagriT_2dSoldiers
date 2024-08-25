using UnityEngine;

public interface IGridGenerator
{
    GridCellControl[,] GenerateGrid(int width, int height, GameObject cellPrefab, Transform parent);
}