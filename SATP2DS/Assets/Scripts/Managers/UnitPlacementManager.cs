using System;
using UnityEngine;
using Utilities;

/// <summary>
/// UnitPlacementManager class manages the placement of units on the grid.
/// </summary>
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
        if (Input.GetMouseButtonDown(1))
        {
            CheckForSoldier();
        }
        
        
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
    
    
    void CheckForSoldier()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

        if (hit.collider != null)
        {
            var soldier = hit.collider.GetComponentInParent<Soldier1Unit>() || hit.collider.GetComponentInParent<Soldier2Unit>() || hit.collider.GetComponentInParent<Soldier3Unit>();
            if (soldier)
            {
                hit.collider.GetComponentInParent<BaseUnit>().TakeDamage(1);
            }
            else
            {
                Debug.Log("Soldier bileşeni bulunamadı." + hit.collider.name);
            }
        }
    }
    
}