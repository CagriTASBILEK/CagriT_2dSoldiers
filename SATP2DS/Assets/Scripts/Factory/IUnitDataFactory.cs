using System.Collections.Generic;
using Scriptables;
using UnityEngine;

namespace Factory
{
    public interface IUnitDataFactory
    {
        GameObject CreateUnit(UnitData unitData, Vector3 position, Quaternion rotation,Transform parent);
        UnitData GetUnitData(string unitName);
        List<UnitData> GetAllUnitData();
    }
}