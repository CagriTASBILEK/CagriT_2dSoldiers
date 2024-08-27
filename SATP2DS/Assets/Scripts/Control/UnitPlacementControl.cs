using Interfaces;
using Managers;
using Scriptables;
using Unit;
using UnityEngine;
using Utilities;

namespace Control
{
    /// <summary>
    /// UnitPlacementControl class implements the IUnitPlacementService interface
    /// and is responsible for handling unit placement on the grid.
    /// </summary>
    public class UnitPlacementControl : IUnitPlacementService
    {
        private readonly GridManager _gridManager;

        /// <summary>
        /// Constructor for UnitPlacementControl.
        /// </summary>
        /// <param name="gridManager">The GridManager instance.</param>
        public UnitPlacementControl(GridManager gridManager)
        {
            _gridManager = gridManager;
        }

        /// <summary>
        /// Checks if a unit can be placed at the specified position.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        /// <param name="size">The size of the unit.</param>
        /// <returns>True if the unit can be placed, false otherwise.</returns>
        public bool CanPlaceUnit(int x, int y, Vector2Int size)
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    if (!_gridManager._gridCells[x + i, y + j].IsEmpty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Places a unit at the specified position.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        /// <param name="unitData">The data of the unit to be placed.</param>
        public void PlaceUnit(int x, int y, UnitData unitData)
        {
            for (int i = 0; i < unitData.size.x; i++)
            {
                for (int j = 0; j < unitData.size.y; j++)
                {
                    _gridManager._gridCells[x + i, y + j].Occupy();
                }
            }
            Vector3 unitPosition = _gridManager.GetWorldPosition(x, y);
            GameObject unitObject = FactoryManager.BuildingFactory.CreateUnit(unitData,new Vector2(x, y), Quaternion.identity,null);
            unitObject.transform.position = unitPosition;
            unitObject.GetComponent<BaseUnit>().OnUnitDestroyed += () => ReleaseUnit(x, y, unitData.size);
        }

        /// <summary>
        /// Releases the grid cells occupied by the unit.
        /// </summary>
        /// <param name="x">The x-coordinate of the position.</param>
        /// <param name="y">The y-coordinate of the position.</param>
        /// <param name="size">The size of the unit.</param>
        private void ReleaseUnit(int x, int y, Vector2Int size)
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    _gridManager._gridCells[x + i, y + j].Release();
                }
            }
        }
    }
}