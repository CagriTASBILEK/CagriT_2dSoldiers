using UnityEngine;

public class UnitPlacementController : IUnitPlacementService
{
    private readonly GridManager _gridManager;

    public UnitPlacementController(GridManager gridManager)
    {
        _gridManager = gridManager;
    }

    public bool CanPlaceUnit(int x, int y, Vector2Int size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (!_gridManager._gridCells[x+i,y+j].IsEmpty)
                {
                    return false;
                }
            }
        }
        return true;
    }

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
        GameObject unitObject = Object.Instantiate(unitData.unitPrefab, new Vector2(x, y), Quaternion.identity);
        unitObject.transform.position = unitPosition;
    }
}