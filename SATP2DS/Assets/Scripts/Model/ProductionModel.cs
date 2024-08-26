using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace Model
{
    public class ProductionModel
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
}
