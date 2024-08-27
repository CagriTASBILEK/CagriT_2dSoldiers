using System.Collections.Generic;
using Controller;
using Model;
using Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace View
{
    public class ProductionView : SingletonBehaviour<ProductionView>
    {
        public RectTransform contentPanel; 
        public GameObject buildingButtonPrefab;
        public InfiniteScroll infiniteScroll;
        private List<GameObject> _buildingButtons = new List<GameObject>();

        [HideInInspector] public ProductionModel productionModel;
        [HideInInspector] public ProductionController productionController;
    
    
        private void Start()
        {
            productionModel = new ProductionModel();
            productionController = new ProductionController(productionModel,this);
        
            DisplayBuildings(productionModel.buildingFactory, productionController.OnBuildingSelected);
        }
    
        public void DisplayBuildings(List<UnitData> buildingList, System.Action<UnitData> onBuildingSelected)
        {
            foreach (var buildingData in buildingList)
            {
                GameObject newButton = Instantiate(buildingButtonPrefab, contentPanel);
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = buildingData.unitDisplayName;
                newButton.GetComponent<Image>().sprite = buildingData.unitIcon;
                newButton.GetComponent<Button>().onClick.AddListener(() => onBuildingSelected(buildingData));
                _buildingButtons.Add(newButton);
            }

            StartCoroutine(infiniteScroll.Initialize());
        }

        public void ClearBuildingButtons()
        {
            foreach (var button in _buildingButtons)
            {
                Destroy(button);
            }
            _buildingButtons.Clear();
        }
    
    }
}
