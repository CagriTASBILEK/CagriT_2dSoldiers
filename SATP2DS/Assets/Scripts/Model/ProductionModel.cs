using System.Collections.Generic;
using Managers;
using Scriptables;
using UnityEngine;

namespace Model
{
    public class ProductionModel
    {
        public List<UnitData> buildingFactory;
        public ProductionModel()
        {
            LoadBuildings();
        }

        private void LoadBuildings()
        {
            buildingFactory = FactoryManager.BuildingFactory.GetAllUnitData();
        }
    }
}
