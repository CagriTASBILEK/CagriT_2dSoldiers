using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitData", menuName = "Units/UnitData", order = 0)]

public class UnitData : ScriptableObject
{
    public String unitDisplayName;
    public Sprite unitIcon;
    public GameObject unitPrefab;
    public Vector2Int size;
    public bool hasSoldier;
}

