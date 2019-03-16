
namespace SolarTraders
{
    public abstract class Ship
    {
        protected Ship(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class Probe : Ship
    {
        private static int numProbes = 0;

        public Probe(string Name) : base(Name)
        {

        }

        public static string GetProbeName()
        {
            string toRet = Probe.numProbes.ToString();
            numProbes++;
            return toRet;
        }
    }

    public class Drone : Ship
    {
        private static int numDrones = 0;

        public enum Type { Mining, Refining, Farming }

        public Drone(string Name, Drone.Type Type) : base(Name)
        {
            type = Type;
        }

        Type type { get; }
        public static string GetDroneName()
        {
            string toRet = Drone.numDrones.ToString();
            numDrones++;
            return toRet;
        }
    }

    public class ColonyShip : Ship
    {
        private static int numColonyShips = 0;

        public ColonyShip(string name) : base(name)
        {

        }

        public static string GetColonyShipName()
        {
            string toRet = ColonyShip.numColonyShips.ToString();
            numColonyShips++;
            return toRet;
        }
    }
}
