using System;
using UnityEngine;
using System.Collections.Generic;
using Factory;
using Scriptables;
using Utilities;


public enum UnitType
{
    Building,
    Soldier
}

public class UnitDataFactory : IUnitDataFactory
{ 
    private static Dictionary<UnitType, UnitDataFactory> _instances = new Dictionary<UnitType, UnitDataFactory>();
    private Dictionary<string, UnitData> unitDataCache;
    
    private UnitDataFactory(string folderPath)
    {
        LoadData(folderPath);
    }
    
    public static UnitDataFactory GetInstance(UnitType type)
    {
        if (!_instances.ContainsKey(type))
        {
            string folderPath = type == UnitType.Building ? "BuildingDatas/" : "SoldierDatas/";
            _instances[type] = new UnitDataFactory(folderPath);
        }
        
        return _instances[type];
    }

    private void LoadData(string path)
    {
        unitDataCache = new Dictionary<string, UnitData>();
        UnitData[] unitDatas = Resources.LoadAll<UnitData>(path);
        foreach (var data in unitDatas)
        {
            if (data != null)
            {
                unitDataCache[data.unitDisplayName] = data;
            }
        }
    }
    public GameObject CreateUnit(UnitData unitData, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (unitData == null)
            throw new ArgumentNullException(nameof(unitData));

        GameObject unitObject = unitData.unitPrefab.Spawn(position, rotation, parent);
        unitObject.name = unitData.unitDisplayName;
        
        return unitObject;
    }
    public UnitData GetUnitData(string unitName)
    {
        unitDataCache.TryGetValue(unitName, out UnitData unitData);
        if (unitData == null)
        {
            Debug.LogError($"UnitData not found for: {unitName}");
        }
        return unitData;
    }

    public List<UnitData> GetAllUnitData()
    {
        return new List<UnitData>(unitDataCache.Values);
    }
}