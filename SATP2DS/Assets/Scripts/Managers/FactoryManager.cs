namespace Managers
{
    public class FactoryManager
    {
        public static UnitDataFactory BuildingFactory { get; private set; }
        public static UnitDataFactory SoldierFactory { get; private set; }

        public static void Initialize()
        {
            BuildingFactory = UnitDataFactory.GetInstance(UnitType.Building);
            SoldierFactory = UnitDataFactory.GetInstance(UnitType.Soldier);
        }
    }
}
