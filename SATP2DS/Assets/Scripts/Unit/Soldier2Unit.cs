using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier2Unit : BaseUnit
{
    public int AttackDamage { get; private set; }

    public Soldier2Unit(int attackDamage)
    {
        AttackDamage = attackDamage;
        Health = 20; 
    }

    public override void UnitAction()
    {
    }

    public void Attack(BaseUnit target)
    {
        target.TakeDamage(AttackDamage);
    }
}
