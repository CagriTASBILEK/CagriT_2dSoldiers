using UnityEngine;

public class GridCellController
{
    public GameObject CellObject { get; private set; }
    public bool IsEmpty { get; private set; }

    public GridCellController(GameObject cellObject)
    {
        CellObject = cellObject;
        IsEmpty = true;
    }

    public void Occupy()
    {
        IsEmpty = false;
    }

    public void Release()
    {
        IsEmpty = true;
    }
}