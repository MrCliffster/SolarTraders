using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SolarTraders;
using System;

namespace SolarTraders
{
    /// <summary>
    /// Universe is a Singleton model of the in-game universe.
    /// </summary>
    public class Universe
    {
        private static Universe instance = new Universe();

        public static Universe Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Universe();
                }
                return instance;
            }
        }

        // Constructor
        private Universe(int systemCount = 1)
        {
            Systems = new List<SolarSystem>(systemCount);
        }

        public List<SolarSystem> Systems { get; }

        private int numberSystems;

        public void AddSolarSystem()
        {
            Systems.Add(new SolarSystem("medium"));
        }

    }

    public class SolarSystem
    {
        public List<PlanetaryBody> Bodies { get; }

        public Dictionary<PlanetaryBody, GameObject> BodyToObjectMap { get; }

        string size; // Change to enum? One of: Small/Med/Large/Huge
        int maximum_radius = 100;

        public int Maximum_radius
        {
            get
            {
                return maximum_radius;
            }
        }

        int numberBodies; // max number bodies = size max + 1 for star

        // Constructor
        public SolarSystem(string size)
        {
            this.size = size;
            Bodies = new List<PlanetaryBody>
            {
                new Star(Star.GetStarType(), "Default Star")
            };

            BodyToObjectMap = new Dictionary<PlanetaryBody, GameObject>();

            GeneratePlanets();
        }

        private void GeneratePlanets()
        {
            int numPlanets = 3;
            switch (size)
            {
                case "small":
                    numPlanets = UnityEngine.Random.Range(3, 5);
                    break;
                case "medium":
                    numPlanets = UnityEngine.Random.Range(4, 6);
                    break;
                case "large":
                    numPlanets = UnityEngine.Random.Range(5, 7);
                    break;
                case "huge":
                    numPlanets = UnityEngine.Random.Range(7, 11);
                    break;
            }

            for (int i = 0; i < numPlanets; i++)
            {
                if (i == 2) // third rock from the sun! Randomise this eventually
                {
                    Bodies.Add(new Planet(true, Planet.GetPlanetType(), i.ToString()));
                }
                else
                {
                    Bodies.Add(new Planet(false, Planet.GetPlanetType(), i.ToString()));
                }
            }
        }

        public PlanetaryBody GetBodyFromGameObject(GameObject obj)
        {
            if (BodyToObjectMap.ContainsValue(obj))
            {
                var key = BodyToObjectMap.Where(item => item.Value.Equals(obj)).Select(item => item.Key).FirstOrDefault();
                return key;
            }
            return null;
        }
    }

    public abstract class PlanetaryBody
    {
        public string Type { get;  }
        public string Name { get;  }

        public PlanetaryBody(string type, string name)
        {
            this.Type = type;
            this.Name = name;
        }

        public override string ToString()
        {
            return GetType().Name + Name;
        }
    }

    public class Star : PlanetaryBody
    {
        // TODO: Change to enum? Red dwarf, neutron, black hole, regular
        public enum StarType {Yellow, Neutron, BlackHole, RedDwarf}

        public Star(string type, string name) : base(type, name)
        {
            // Pass
        }

        public static string GetStarType()
        {
            // Insert random star type logic here?
            return "yellow";
        }

        public override string ToString()
        {
            return "Star " + Name;
        }
    }

    public class Planet : PlanetaryBody
    {
        List<ResourceDeposit> deposits;
        List<Moon> moons;
        public bool Colonised;
        public bool hasBelt;

        // Constructor
        public Planet(bool isColonised, string type, string name) : base(type, name)
        {
            deposits = new List<ResourceDeposit>();
            moons = new List<Moon>();
            Colonised = isColonised;
            MakeMoons();
            GenerateResources();
        }

        public void AddDeposit(ResourceStockpile.ResourceType type, ResourceDeposit.Size size)
        {
            deposits.Add(new ResourceDeposit(type, size));
        }

        private void MakeMoons()
        {
            for (int i = 0; i < UnityEngine.Random.Range(0, 2); i++)
            {
                moons.Add(new Moon(i.ToString()));
            }
        }

        private void GenerateResources()
        {
            switch (Type)
            {
                case "solid":
                    AddDeposits(ResourceStockpile.ResourceType.Metals, UnityEngine.Random.Range(0, 2));
                    AddDeposits(ResourceStockpile.ResourceType.Gasses, UnityEngine.Random.Range(1, 4));
                    AddDeposits(ResourceStockpile.ResourceType.Food, UnityEngine.Random.Range(2, 10));
                    break;
                case "gas":
                    break;
                case "asteroid":
                    break;
            }
        }

        public void AddDeposits(ResourceStockpile.ResourceType type, int number)
        {
            int randomSize;
            ResourceDeposit.Size size;

            for (int i = 0; i < number; i++)
            {

                randomSize = UnityEngine.Random.Range(0, 3);

                switch (randomSize)
                {
                    case 0:
                        size = ResourceDeposit.Size.Small;
                        break;
                    case 1:
                        size = ResourceDeposit.Size.Medium;
                        break;
                    case 2:
                        size = ResourceDeposit.Size.Large;
                        break;
                    case 3:
                        size = ResourceDeposit.Size.Huge;
                        break;
                    default:
                        size = ResourceDeposit.Size.Medium;
                        break;

                }

                AddDeposit(type, size);
            }
        }

        public static string GetPlanetType()
        {
            return "solid"; // change to include gas, asteroid
        }

        public override string ToString()
        {
            string depositstrings = "";
            foreach (ResourceDeposit dep in deposits)
            {
                depositstrings += dep.ToString() + ", ";
            }
            return "Planet '" + Name + "' with the following deposits:" + depositstrings;
        }
    }

    public class Moon : PlanetaryBody
    {
        public Moon(string name, string type = "lunar") : base(type, name)
        {
            // Pass
        }
    }
}

public class ResourceDeposit
{
    public enum Size { Small, Medium, Large, Huge}
    ResourceStockpile.ResourceType type;
    Size size;
    float startingSize;
    public ResourceDeposit(ResourceStockpile.ResourceType type, Size size)
    {
        this.type = type;
        this.size = size;
        switch (size) // Add in configuration file system to change the hard-coded values
        {
            case Size.Small:
                startingSize = 200;
                break;
            case Size.Medium:
                startingSize = 500;
                break;
            case Size.Large:
                startingSize = 1000;
                break;
            case Size.Huge:
                startingSize = 2000;
                break;
        }

    }

    public override string ToString()
    {
        return "Resource Deposit of " + startingSize + " " + type;
    }
}


