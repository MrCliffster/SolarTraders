using SolarTraders;
using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    // Managers
    private ShipManager shipMan;

    public List<Planet> colonisedPlanets;
    public List<PlanetaryBody> exploredPlanets;

    public ResourceStockpile resPool;
    public int GasesRateOfChange;
    public int FoodRateOfChange;
    public int MetalsRateOfChange;

    public float tickDuration = 1.0f;
    private float currentTime;

    // Awake is called before Start
    private void Awake()
    {
        colonisedPlanets = new List<Planet>();
        resPool = new ResourceStockpile();
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
        while (Time.time - currentTime > tickDuration)
        {
            yield return new WaitForSeconds(Time.time - currentTime);
        }
        TickResources();
    }

    private void TickResources()
    {
        Debug.Log("Resources Ticked!");
        resPool.AddResource(ResourceStockpile.ResourceType.Food, FoodRateOfChange);
        resPool.AddResource(ResourceStockpile.ResourceType.Gasses, GasesRateOfChange);
        resPool.AddResource(ResourceStockpile.ResourceType.Metals, MetalsRateOfChange);

        UIResourceManager.GetInstance().SetAllROCs(GasesRateOfChange, MetalsRateOfChange, FoodRateOfChange);
        UIResourceManager.GetInstance().SetAll(resPool.gasses, resPool.metals, resPool.food);

        currentTime = Time.time;
        StartCoroutine(ResourceTick());
    }
}
