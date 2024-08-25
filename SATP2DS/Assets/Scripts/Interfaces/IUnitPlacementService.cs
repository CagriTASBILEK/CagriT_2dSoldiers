using UnityEngine;

public interface IUnitPlacementService
{
    bool CanPlaceUnit(int x, int y, Vector2Int size);
    void PlaceUnit(int x, int y, UnitData unitData);
}