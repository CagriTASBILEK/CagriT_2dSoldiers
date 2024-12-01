using UnityEngine;

namespace Control
{
    /// <summary>
    /// GridCellControl class manages the state of a grid cell.
    /// </summary>
    public class GridCellControl
    {
        /// <summary>
        /// The GameObject representing the cell.
        /// </summary>
        public GameObject CellObject { get; private set; }
    
        /// <summary>
        /// Indicates whether the cell is empty.
        /// </summary>
        public bool IsEmpty { get; private set; }

        // A* Pathfinding properties
        public float GCost { get; set; }
        public float HCost { get; set; }
        public GridCellControl Parent { get; set; }
        public float FCost => GCost + HCost;

        /// <summary>
        /// Constructor for GridCellControl.
        /// </summary>
        /// <param name="cellObject">The GameObject representing the cell.</param>
        public GridCellControl(GameObject cellObject)
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
}