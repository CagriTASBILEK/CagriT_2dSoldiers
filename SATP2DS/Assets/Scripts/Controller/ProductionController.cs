using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionController : MonoBehaviour
{
    private ProductionModel _model;
    private ProductionView _view;

    private void Start()
    {
        _model = new ProductionModel();
        _view = FindObjectOfType<ProductionView>();
        _view.DisplayBuildings(_model.BuildingList, OnBuildingSelected);
    }

    private void OnBuildingSelected(UnitData selectedBuilding)
    {
        UnitPlacementManager.Instance.selectedUnitData = selectedBuilding;
    }
}
