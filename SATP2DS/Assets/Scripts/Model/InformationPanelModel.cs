using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace Model
{
    public class InformationPanelModel
    { 
        public string BuildingName { get; private set; }
        public Sprite BuildingSprite { get; private set; }
        public List<UnitData> Soldiers { get; private set; }
    
        public InformationPanelModel()
        {
            LoadSoldiers();
        }
        private void LoadSoldiers()
        {
            Soldiers = new List<UnitData>(Resources.LoadAll<UnitData>("SoldierDatas/"));
        }
    
    }
}