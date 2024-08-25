using UnityEngine;

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
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        OnUnitDestroyed?.Invoke();
    }
    public abstract void UnitAction();
}