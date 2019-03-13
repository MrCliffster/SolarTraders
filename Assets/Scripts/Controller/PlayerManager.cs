using SolarTraders;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public UIResourceManager uiResMan;
    public ResourceStockpile resPool;
    // Start is called before the first frame update
    void Start()
    {
        resPool = new ResourceStockpile();
        resPool.AddResource(0, 100); //TODO: Get these from setup
        uiResMan.UpdateGasText(100);
        uiResMan.UpdateFoodText(100);
        uiResMan.UpdateMetalText(100);
        Debug.Log("Updated all resource texts!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
