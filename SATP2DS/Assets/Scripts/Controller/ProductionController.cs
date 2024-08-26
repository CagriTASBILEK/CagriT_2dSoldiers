using Managers;
using Model;
using Scriptables;
using View;

namespace Controller
{
    public class ProductionController
    {
        private ProductionModel _model;
        private ProductionView _view;
    
        public ProductionController(ProductionModel model, ProductionView view)
        {
            _model = model;
            _view = view;
        }
    
        public void OnBuildingSelected(UnitData selectedBuilding)
        {
            UnitPlacementManager.Instance.selectedUnitData = selectedBuilding;
            InformationPanelView.Instance.informationController.UpdateView(selectedBuilding.unitDisplayName,selectedBuilding.unitIcon,selectedBuilding.hasSoldier);
        }
    }
}
