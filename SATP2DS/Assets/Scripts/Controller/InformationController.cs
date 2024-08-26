using System.Collections.Generic;
using Model;
using Scriptables;
using UnityEngine;
using View;

namespace Controller
{
    public class InformationPanelController
    {
        private readonly InformationPanelModel _model;
        private readonly InformationPanelView _view;

        public InformationPanelController(InformationPanelModel model, InformationPanelView view)
        {
            _model = model;
            _view = view;
        }
    
        public void UpdateView(string buildingName, Sprite buildingSprite,bool hasSoldier = false)
        {
            _view.UpdateBuildingInfo(buildingName, buildingSprite, hasSoldier ? _model.Soldiers : null);
        }

        public void SetSoldiers(List<UnitData> soldiers)
        {
            _view.UpdateBuildingInfo(_model.BuildingName, _model.BuildingSprite, soldiers);
        }
    }
}
