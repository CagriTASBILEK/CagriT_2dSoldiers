using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier3Unit : BaseUnit
{
    public int AttackDamage { get; private set; }

    public Soldier3Unit(int attackDamage)
    {
        AttackDamage = attackDamage;
        Health = 30; 
    }

    public override void UnitAction()
    {
    }

    public void Attack(BaseUnit target)
    {
        target.TakeDamage(AttackDamage);
    }
}
