using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantUnit : BaseUnit
{
    public override void UnitAction()
    {
        Destroy(gameObject,5);
    }
}
