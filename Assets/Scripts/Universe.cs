using System;
using System.Collections.Generic;

namespace SolarTraders
{
    public class Universe
    {
        public List<SolarSystem> Systems { get; }

        private int numberSystems;

        public Universe(int systemCount = 1)
        {
            Systems = new List<SolarSystem>(systemCount);
        }

        public void AddSolarSystem()
        {
            Systems.Add(new SolarSystem("medium"));
        }
    }

    public class SolarSystem
    {
        public List<PlanetaryBody> Bodies { get; }


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
        public SolarSystem(string size)
        {
            this.size = size;
            Bodies = new List<PlanetaryBody>
            {
                new Star(Star.GetStarType())
            };
            GeneratePlanets();
        }

        private void GeneratePlanets()
        {
            Random random = new Random();
            int numPlanets = 3;
            switch (size)
            {
                case "small":
                    numPlanets = random.Next(3, 5);
                    break;
                case "medium":
                    numPlanets = random.Next(4, 6);
                    break;
                case "large":
                    numPlanets = random.Next(5, 7);
                    break;
                case "huge":
                    numPlanets = random.Next(7, 11);
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
    }

    public abstract class PlanetaryBody
    {
        string name;
        string type;
    }

    public class Star : PlanetaryBody
    {
        string type; // Change to enum? Red dwarf, neutron, black hole, regular
        string name;
        public Star(string type)
        {
            this.type = type;
        }

        public static string GetStarType()
        {
            // Insert random star type logic here?
            return "regular";
        }
    }

    public class Planet : PlanetaryBody
    {
        string name;
        string type; // Change to enum?
        List<ResourceDeposit> deposits;
        List<Moon> moons;
        bool colonised;
        public bool hasBelt;

        // Constructor
        public Planet(bool isColonised, string type)
        {
            deposits = new List<ResourceDeposit>();
            moons = new List<Moon>();
            colonised = isColonised;
            this.type = type;
            MakeMoons();

        }

        public void addDeposit(string type, string size)
        {
            deposits.Add(new ResourceDeposit(type, size));
        }

        private void MakeMoons()
        {
            Random random = new Random();
            for (int i = 0; i < random.Next(0, 2); i++)
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
        public Moon()
        {
            // Fill out later
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


