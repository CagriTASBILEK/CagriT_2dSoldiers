using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionModel : MonoBehaviour
{
    public List<UnitData> BuildingList { get; private set; }

    public ProductionModel()
    {
        LoadBuildings();
    }

    private void LoadBuildings()
    {
        BuildingList = new List<UnitData>(Resources.LoadAll<UnitData>("BuildingDatas/"));
    }
}
