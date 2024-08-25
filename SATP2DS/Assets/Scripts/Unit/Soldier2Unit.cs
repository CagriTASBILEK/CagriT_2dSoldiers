using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier2Unit : BaseUnit
{
    public int AttackDamage;
    
    public override void UnitAction()
    {
    }

    public void Attack(BaseUnit target)
    {
        target.TakeDamage(AttackDamage);
    }
}
