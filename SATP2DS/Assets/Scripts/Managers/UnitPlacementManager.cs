using System;
using UnityEngine;
using Utilities;

public class UnitPlacementManager : SingletonBehaviour<UnitPlacementManager>
{
    public UnitData selectedUnitData;
    public IUnitPlacementService placementService;
    public GridManager gridManager;
    private Vector3 mousePosition;
    private void Start()
    {
        placementService = new UnitPlacementController(gridManager);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3Int gridPosition = new Vector3Int(Mathf.RoundToInt(mousePosition.x), Mathf.RoundToInt(mousePosition.y), 0);
            
            if (placementService.CanPlaceUnit(gridPosition.x, gridPosition.y, selectedUnitData.size))
            {
                placementService.PlaceUnit(gridPosition.x, gridPosition.y,selectedUnitData);
            }
            else
            {
                Debug.Log("Cannot place unit here.");
            }
        }
    }
    
}