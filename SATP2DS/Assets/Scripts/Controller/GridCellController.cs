using UnityEngine;

/// <summary>
/// GridCellController class manages the state of a grid cell.
/// </summary>
public class GridCellController
{
    /// <summary>
    /// The GameObject representing the cell.
    /// </summary>
    public GameObject CellObject { get; private set; }
    
    /// <summary>
    /// Indicates whether the cell is empty.
    /// </summary>
    public bool IsEmpty { get; private set; }

    /// <summary>
    /// Constructor for GridCellController.
    /// </summary>
    /// <param name="cellObject">The GameObject representing the cell.</param>
    public GridCellController(GameObject cellObject)
    {
        CellObject = cellObject;
        IsEmpty = true;
    }

    /// <summary>
    /// Marks the cell as occupied.
    /// </summary>
    public void Occupy()
    {
        IsEmpty = false;
    }

    /// <summary>
    /// Marks the cell as empty.
    /// </summary>
    public void Release()
    {
        IsEmpty = true;
    }
}