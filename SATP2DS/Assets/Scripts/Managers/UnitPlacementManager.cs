using System;
using UnityEngine;

public class UnitPlacementManager : MonoBehaviour
{
    public UnitData selectedUnitData;
    private IUnitPlacementService placementService;
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
            
            Debug.Log(gridPosition);
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