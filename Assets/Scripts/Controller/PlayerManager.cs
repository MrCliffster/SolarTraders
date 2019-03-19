using SolarTraders;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerManager : MonoBehaviour
{
    // Managers
    public ShipManager shipMan;

    public List<Planet> colonisedPlanets;
    public List<PlanetaryBody> exploredPlanets;
    public List<ResourceCollector> collectors;

    public ResourceStockpile resPool;
    public int GasesRateOfChange;
    public int FoodRateOfChange;
    public int MetalsRateOfChange;

    public float tickDuration = 100.0f;
    private float currentTime;

    // Awake is called before Start
    private void Awake()
    {
        colonisedPlanets = new List<Planet>();
        resPool = new ResourceStockpile();
        collectors = new List<ResourceCollector>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UIResourceManager UIResMan = UIResourceManager.GetInstance();
        shipMan = GetComponent<ShipManager>();

        resPool.AddResource(0, 100); //TODO: Get these from setup

        UIResMan.SetGasText(100);
        UIResMan.SetFoodText(100);
        UIResMan.SetMetalText(100);

        currentTime = Time.time;
        StartCoroutine(ResourceTick());
    }

    private IEnumerator<WaitForSeconds> ResourceTick()
    {
        while (Time.time - currentTime < tickDuration)
        {
            yield return new WaitForSeconds(Time.time - currentTime);
        }
        TickResources();
        yield return null;
    }

    private void TickResources()
    {
        UpdateROC();

        resPool.AddResource(ResourceStockpile.ResourceType.Food, FoodRateOfChange);
        resPool.AddResource(ResourceStockpile.ResourceType.Gasses, GasesRateOfChange);
        resPool.AddResource(ResourceStockpile.ResourceType.Metals, MetalsRateOfChange);

        UIResourceManager.GetInstance().SetAllROCs(GasesRateOfChange, MetalsRateOfChange, FoodRateOfChange);
        UIResourceManager.GetInstance().SetAll(resPool.gasses, resPool.metals, resPool.food);

        currentTime = Time.time;
        StartCoroutine(ResourceTick());
    }

    private void UpdateROC()
    {
        int numPlanets = colonisedPlanets.Count;
        int numShips = shipMan.GetNumShips();

        int FoodToBe = 0;
        int MetalsToBe = 0;
        int GasesToBe = 0;

        // Take away food needed
        FoodToBe -= (3 * numPlanets);

        // Take away ship maintenance
        MetalsToBe -= (2 * numShips);
        GasesToBe -= numShips;

        foreach (ResourceCollector col in collectors)
        {
            switch (col.type)
            {
                case ResourceStockpile.ResourceType.Food:
                    FoodToBe += Farm.collectionRate;
                    break;
                case ResourceStockpile.ResourceType.Gasses:
                    GasesToBe += Refinery.collectionRate;
                    break;
                case ResourceStockpile.ResourceType.Metals:
                    MetalsToBe += Mine.collectionRate;
                    break;
                default:
                    Debug.LogError("No appropriate type for Resource Collector!");
                    break;
            }

        }
    }
}
