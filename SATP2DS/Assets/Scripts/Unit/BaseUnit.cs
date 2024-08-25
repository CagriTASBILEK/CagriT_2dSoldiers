using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public int Health { get; protected set; }

    public delegate void UnitDestroyed();
    public event UnitDestroyed OnUnitDestroyed;

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
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