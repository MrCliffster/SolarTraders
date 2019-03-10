using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
                new Star(Star.GetStarType())
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
                    numPlanets = Random.Range(3, 5);
                    break;
                case "medium":
                    numPlanets = Random.Range(4, 6);
                    break;
                case "large":
                    numPlanets = Random.Range(5, 7);
                    break;
                case "huge":
                    numPlanets = Random.Range(7, 11);
                    break;
            }

            for (int i = 0; i < numPlanets; i++)
            {
                if (i == 2) // third rock from the sun! Randomise this eventually
                {
                    Bodies.Add(new Planet(true, Planet.GetPlanetType()));
                }
                else
                {
                    Bodies.Add(new Planet(false, Planet.GetPlanetType()));
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
        string Type;
        string Name;

        public PlanetaryBody(string type)
        {
            this.Type = type;
        }

        public override string ToString()
        {
            return GetType().Name + Name;
        }
    }

    public class Star : PlanetaryBody
    {
        // Type = Change to enum? Red dwarf, neutron, black hole, regular

        public Star(string type) : base(type)
        {
            // Pass
        }

        public static string GetStarType()
        {
            // Insert random star type logic here?
            return "yellow";
        }
    }

    public class Planet : PlanetaryBody
    {
        List<ResourceDeposit> deposits;
        List<Moon> moons;
        bool Colonised;
        public bool hasBelt;

        // Constructor
        public Planet(bool isColonised, string type) : base(type)
        {
            deposits = new List<ResourceDeposit>();
            moons = new List<Moon>();
            Colonised = isColonised;
            MakeMoons();
        }

        public void AddDeposit(string type, string size)
        {
            deposits.Add(new ResourceDeposit(type, size));
        }

        private void MakeMoons()
        {
            for (int i = 0; i < Random.Range(0, 2); i++)
            {
                moons.Add(new Moon());
            }
        }

        public static string GetPlanetType()
        {
            return "solid"; // change to include gas, asteroid
        }
    }

    public class Moon : PlanetaryBody
    {
        public Moon(string type = "lunar") : base(type)
        {
            // Pass
        }
    }
}

public class ResourceDeposit
{
    string type; // Change to enum? One of Metals, Gasses, Food
    string size; // Change to enum? One of: Small/Med/Large/Huge
    float startingSize;
    public ResourceDeposit(string type, string size)
    {
        this.type = type;
        this.size = size;
        switch (size) // Add in configuration file system to change the hard-coded values
        {
            case "small":
                startingSize = 200;
                break;
            case "medium":
                startingSize = 500;
                break;
            case "large":
                startingSize = 1000;
                break;
            case "huge":
                startingSize = 2000;
                break;
        }

    }
}


