using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

/// <summary>
/// UnitPlacementManager class manages the placement of units on the grid.
/// </summary>
public class UnitPlacementManager : SingletonBehaviour<UnitPlacementManager>
{
    [HideInInspector] public UnitData selectedUnitData;
    public IUnitPlacementService placementService;
    public GridManager gridManager;

    private Vector3 mousePosition;
    EventSystem m_EventSystem;

    public SeekerControl selectedSoldier;

    private void Start()
    {
        placementService = new UnitPlacementControl(gridManager);
        m_EventSystem = EventSystem.current;
        selectedUnitData = null;
        selectedSoldier = null;
    }


    void Update()
    {
        HandleSelection();
        HandlePlacement();
    }

    private void HandlePlacement()
    {
        if (Input.GetMouseButtonDown(0) && selectedUnitData)
        {
            if (m_EventSystem.currentSelectedGameObject == null || m_EventSystem.currentSelectedGameObject.layer != 5)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;

                Vector3Int gridPosition = new Vector3Int(Mathf.RoundToInt(mousePosition.x),
                    Mathf.RoundToInt(mousePosition.y), 0);

                if (gridPosition.x >= 0 && gridPosition.y >= 0 &&
                    gridPosition.x + selectedUnitData.size.x <= gridManager.width &&
                    gridPosition.y + selectedUnitData.size.y <= gridManager.height)
                {
                    if (placementService.CanPlaceUnit(gridPosition.x, gridPosition.y, selectedUnitData.size))
                    {
                        placementService.PlaceUnit(gridPosition.x, gridPosition.y, selectedUnitData);
                        selectedUnitData = null; // Clear selection after placement
                    }
                    else
                    {
                        PlacementView.Instance.placementController.ShowInvalidPlacementIndicator(gridPosition,selectedUnitData);
                        Debug.Log("Cannot place unit here.");
                    }
                }
                else
                {
                    selectedSoldier = null;
                    selectedUnitData = null;
                    Debug.Log("Out of grid bounds.");
                }
            }
        }
    }

    private void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitLC = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

            if (hitLC.collider != null)
            {
                Transform hitTransform = hitLC.collider.transform;

                var soldier = hitLC.collider.GetComponentInParent<Soldier1Unit>() ||
                              hitLC.collider.GetComponentInParent<Soldier2Unit>() ||
                              hitLC.collider.GetComponentInParent<Soldier3Unit>();
                if (soldier)
                {
                    SelectSoldier(hitTransform.GetComponentInParent<SeekerControl>());
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitRC = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

            if (hitRC.collider != null)
            {
                Transform hitTransform = hitRC.collider.transform;

                if (hitTransform.GetComponentInParent<PowerPlantUnit>())
                {
                    SelectTargetBuilding(hitTransform.GetComponentInParent<PowerPlantUnit>());
                }
                else
                {
                    SelectTargetGrid(hitTransform.position);
                }
            }
        }
    }

    private void SelectSoldier(SeekerControl soldier)
    {
        if (soldier != null)
        {
            selectedSoldier = soldier;
        }
    }

    private void SelectTargetBuilding(PowerPlantUnit building)
    {
        if (selectedSoldier != null && building != null)
        {
            Vector3Int targetGridPosition = gridManager.GetGridPosition(building.transform.localPosition);
            selectedSoldier.SetTarget(targetGridPosition);
            Debug.Log($"Target building selected: {building.name}");
        }
    }

    private void SelectTargetGrid(Vector3 pos)
    {
        Vector3Int targetGridPosition = gridManager.GetGridPosition(pos);
        if (selectedSoldier != null && gridManager.IsCellEmpty(targetGridPosition.x, targetGridPosition.y))
        {
            selectedSoldier.SetTarget(targetGridPosition);
        }
        else
        {
            Debug.Log("Target cell is not empty.");
        }
    }
}