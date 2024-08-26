using System.Collections;
using Control;
using UnityEngine;

namespace Unit
{
    public class Soldier1Unit : BaseUnit
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
                if (GetComponent<SeekerControl>().isMoving)
                {
                    yield return new WaitUntil(() => !GetComponent<SeekerControl>().isMoving);
                }
                
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
            return attacker is PowerPlantUnit;
        }
    }
}
