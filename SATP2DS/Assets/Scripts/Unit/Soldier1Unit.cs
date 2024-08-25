using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier1Unit : BaseUnit
{
    public int AttackDamage { get; private set; }

    public Soldier1Unit(int attackDamage)
    {
        AttackDamage = attackDamage;
        Health = 10; 
    }

    public override void UnitAction()
    {
    }

    public void Attack(BaseUnit target)
    {
        target.TakeDamage(AttackDamage);
    }
}
