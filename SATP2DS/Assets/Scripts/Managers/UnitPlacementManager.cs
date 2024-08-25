using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

/// <summary>
/// UnitPlacementManager class manages the placement of units on the grid.
/// </summary>
public class UnitPlacementManager : SingletonBehaviour<UnitPlacementManager>
{
    [HideInInspector]public UnitData selectedUnitData;
    
    public IUnitPlacementService placementService;
    
    public GridManager gridManager;
    
    private Vector3 mousePosition;
    EventSystem m_EventSystem;
    private void Start()
    {
        placementService = new UnitPlacementControl(gridManager);
        m_EventSystem = EventSystem.current;
        selectedUnitData = null;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CheckForSoldier();
        }
        
        if (Input.GetMouseButtonDown(0) && selectedUnitData)
        {
            if (m_EventSystem.currentSelectedGameObject)
            {
                if (m_EventSystem.currentSelectedGameObject.layer == 5)
                    return;
            }
            else
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int gridPosition = new Vector3Int(Mathf.RoundToInt(mousePosition.x),
                    Mathf.RoundToInt(mousePosition.y), 0);

                if (gridPosition.x >= 0 && gridPosition.y >= 0 &&
                    gridPosition.x + selectedUnitData.size.x <= gridManager.width &&
                    gridPosition.y + selectedUnitData.size.y <= gridManager.height)
                {
                    if (placementService.CanPlaceUnit(gridPosition.x, gridPosition.y, selectedUnitData.size))
                    {
                        placementService.PlaceUnit(gridPosition.x, gridPosition.y, selectedUnitData);
                    }
                    else
                    {
                        Debug.Log("Cannot place unit here.");
                    }
                }
                else
                {
                    Debug.Log("Out of grid bounds.");
                }
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