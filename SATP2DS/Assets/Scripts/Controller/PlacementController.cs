using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController
{
    private PlacementView _view;
    private PlacementModel _model;


    public PlacementController(PlacementModel model,PlacementView view)
    {
        _model = model;
        _view = view;
    }

    public void ShowInvalidPlacementIndicator(Vector3 position,UnitData selectedUnitData)
    {
        _view.ShowInvalidPlacementIndicator(position,selectedUnitData);
    }
    
}