using System;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Units/UnitData", order = 0)]

    public class UnitData : ScriptableObject
    {
        public String unitDisplayName;
        public Sprite unitIcon;
        public GameObject unitPrefab;
        public Vector2Int size;
        public bool hasSoldier;
    }
}

