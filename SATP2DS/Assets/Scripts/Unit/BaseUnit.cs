using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public delegate void UnitDestroyed();
    public event UnitDestroyed OnUnitDestroyed;

    private void OnDestroy()
    {
        OnUnitDestroyed?.Invoke();
    }
    public abstract void UnitAction();
}