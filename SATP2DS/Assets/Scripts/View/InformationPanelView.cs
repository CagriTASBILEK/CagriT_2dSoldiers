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
    public class InformationPanelView : SingletonBehaviour<InformationPanelView>
    {
        public TextMeshProUGUI buildingNameText;
        public Image buildingIconImage;
        public Transform soldierInfoContainer;
        public GameObject soldierInfoPrefab;

        private InformationPanelModel informationModel;
        [HideInInspector] public InformationPanelController informationController;
    
        private void Start()
        {
            informationModel = new InformationPanelModel();
            informationController = new InformationPanelController(informationModel,this);
        
            buildingNameText.gameObject.SetActive(false);
            buildingIconImage.gameObject.SetActive(false);
        }
        public void UpdateBuildingInfo(string buildingName, Sprite buildingIcon, List<UnitData> soldiers)
        {
            if (!buildingNameText.gameObject.activeInHierarchy)
            {
                buildingNameText.gameObject.SetActive(true);
                buildingIconImage.gameObject.SetActive(true);
            }
            buildingNameText.text = buildingName;
            buildingIconImage.sprite = buildingIcon;
        
            UpdateSoldierList(soldiers);
        }
    
        private void UpdateSoldierList(List<UnitData> soldiers)
        {
            foreach (Transform child in soldierInfoContainer)
            {
                Destroy(child.gameObject);
            }

            if (soldiers != null && soldiers.Count > 0)
            {
                foreach (var soldier in soldiers)
                {
                    GameObject soldierInfoObject = Instantiate(soldierInfoPrefab, soldierInfoContainer);
                    soldierInfoObject.GetComponentInChildren<TextMeshProUGUI>().text = soldier.unitDisplayName;
                    soldierInfoObject.GetComponent<Image>().sprite = soldier.unitIcon;
                }
            }
        }
    }
}



