using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionView : MonoBehaviour
{
    public RectTransform contentPanel;  // Scroll view içindeki content paneli
    public GameObject buildingButtonPrefab;  // Her bina için buton prefabı
    public InfiniteScroll infiniteScroll;
    private List<GameObject> _buildingButtons = new List<GameObject>();

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
