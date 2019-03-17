
namespace SolarTraders
{
    public abstract class ResourceCollector
    {
        public ResourceStockpile.ResourceType type;
        public readonly int ColonisedBonus = 5;
    }

    public class Refinery : ResourceCollector
    {
        public static int ConstructionCost = 20;

        public readonly int collectionRate = 2;

        public Refinery()
        {
            base.type = ResourceStockpile.ResourceType.Gasses;
        }
    }

    public class Mine : ResourceCollector
    {
        public static int ConstructionCost = 20;

        public readonly int collectionRate = 2;

        public Mine()
        {
            base.type = ResourceStockpile.ResourceType.Metals;
        }
    }

    public class Farm : ResourceCollector
    {
        public static int ConstructionCost = 20;

        public readonly int collectionRate = 2;

        public Farm()
        {
            base.type = ResourceStockpile.ResourceType.Food;
        }
    }
}