
namespace SolarTraders
{
    public abstract class Ship
    {
        public string Name { get; }
    }

    public class Probe : Ship
    {

    }

    public class Drone : Ship
    {
        public string Type { get; }
    }
}
