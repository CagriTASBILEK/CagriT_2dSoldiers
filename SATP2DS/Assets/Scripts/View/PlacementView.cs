using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class PlacementView : SingletonBehaviour<PlacementView>
{
    public Image invalidPlacementIndicator; 
    public TextMeshProUGUI feedbackText;

    private PlacementModel placementModel;
    [HideInInspector] public PlacementController placementController;
    private void Start()
    {
        placementModel = new PlacementModel();
        placementController = new PlacementController(placementModel,this);
        HideInvalidPlacementIndicator();
    }

    public void ShowInvalidPlacementIndicator(Vector3 position,UnitData selectedUnitData)
    {
        if (invalidPlacementIndicator != null)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);
            invalidPlacementIndicator.rectTransform.position = screenPosition;
            invalidPlacementIndicator.gameObject.SetActive(true);

            invalidPlacementIndicator.sprite = selectedUnitData.unitIcon;
            invalidPlacementIndicator.rectTransform.localScale = new Vector3(selectedUnitData.size.x,selectedUnitData.size.y);
            invalidPlacementIndicator.rectTransform.position = screenPosition;
            invalidPlacementIndicator.gameObject.SetActive(true);
            
            if (feedbackText != null)
            {
                feedbackText.text = "'Invalid placement area. Please choose a valid location.'";
                feedbackText.gameObject.SetActive(true);
            }

            StartCoroutine(HideInvalidPlacementIndicator());
        }
    }
    
    public IEnumerator HideInvalidPlacementIndicator()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        
        if (invalidPlacementIndicator != null)
        {
            invalidPlacementIndicator.gameObject.SetActive(false);

            if (feedbackText != null)
            {
                feedbackText.gameObject.SetActive(false);
                feedbackText.text = "";
            }
        }
    }
}
