using UnityEngine;
using Utilities;

namespace Unit
{
    public abstract class BaseUnit : MonoBehaviour
    {
        public int health;
        public delegate void UnitDestroyed();
        public event UnitDestroyed OnUnitDestroyed;
    

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            var otherUnit = other.GetComponent<BaseUnit>();
            if (otherUnit != null && otherUnit != this)
            {
                HandleUnitCollision(otherUnit);
            }
        }
    
        protected virtual void HandleUnitCollision(BaseUnit otherUnit){}
        public virtual void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                Dispose(gameObject);
            }
        }

        private void Dispose(GameObject go)
        {
            ObjectPool.Instance.Recycle(go);
        }
    
        private void OnDisable()
        {
            OnUnitDestroyed?.Invoke();
        }
        public abstract void UnitAction();
    
        public abstract bool CanBeAttackedBy(BaseUnit attacker);
    }
}