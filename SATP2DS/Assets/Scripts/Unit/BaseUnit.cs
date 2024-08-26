using UnityEngine;
using Utilities;

public abstract class BaseUnit : MonoBehaviour
{
    public int health;

    public delegate void UnitDestroyed();
    public event UnitDestroyed OnUnitDestroyed;
    

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
}