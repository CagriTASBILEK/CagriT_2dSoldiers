using System.Collections;
using UnityEngine;

namespace Unit
{
    public class PowerPlantUnit : BaseUnit
    {
        public int AttackDamage;
        public override void UnitAction()
        {
        }
        protected override void HandleUnitCollision(BaseUnit otherUnit)
        {
            if (otherUnit != null && otherUnit.CanBeAttackedBy(this))
            {
                Attack(otherUnit);
            }
        }
    
        public void Attack(BaseUnit target)
        {
            StartCoroutine(AttackRoutine(target));
        }
    
        private IEnumerator AttackRoutine(BaseUnit target)
        {
            while (target != null && target.health > 0)
            {
                if (target.health <= 0 || !target.gameObject.activeInHierarchy)
                {
                    yield break;
                }
                target.TakeDamage(AttackDamage);
                yield return new WaitForSeconds(1);
           
            }
        }
    
        public override bool CanBeAttackedBy(BaseUnit attacker)
        {
            return attacker is Soldier1Unit || attacker is Soldier2Unit || attacker is Soldier3Unit;
        }
    
    }
}
