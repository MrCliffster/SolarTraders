using SolarTraders;
using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    // Managers
    public UIResourceManager uiResMan;
    public ShipManager shipMan;

    public List<Planet> colonisedPlanets;

    public ResourceStockpile resPool;
    // Start is called before the first frame update
    void Start()
    {
        colonisedPlanets = new List<Planet>();
        resPool = new ResourceStockpile();
        resPool.AddResource(0, 100); //TODO: Get these from setup
        uiResMan.UpdateGasText(100);
        uiResMan.UpdateFoodText(100);
        uiResMan.UpdateMetalText(100);
        Debug.Log("Updated all resource texts!");
    }

}
