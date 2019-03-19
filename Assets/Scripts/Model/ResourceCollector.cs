
namespace SolarTraders
{
    public abstract class ResourceCollector
    {
        public ResourceStockpile.ResourceType type;
        public static readonly int ColonisedBonus = 5;
        public bool onColonisedPlanet;
    }

    public class Refinery : ResourceCollector
    {
        public static int ConstructionCost = 20;

        public static readonly int collectionRate = 2;

        public Refinery(bool onColonisedPlanet)
        {
            base.onColonisedPlanet = onColonisedPlanet;
            base.type = ResourceStockpile.ResourceType.Gasses;
        }
    }

    public class Mine : ResourceCollector
    {
        public static int ConstructionCost = 20;

        public static readonly int collectionRate = 2;

        public Mine(bool onColonisedPlanet)
        {
            base.type = ResourceStockpile.ResourceType.Metals;
        }
    }

    public class Farm : ResourceCollector
    {
        public static int ConstructionCost = 20;

        public static readonly int collectionRate = 2;

        public Farm(bool onColonisedPlanet)
        {
            base.type = ResourceStockpile.ResourceType.Food;
        }
    }
}