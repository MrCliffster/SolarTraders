using UnityEngine.UI;
using UnityEngine;

public class UIResourceManager : MonoBehaviour
{
    // ------------Singleton Stuff--------------------
    private static UIResourceManager instance;

    private UIResourceManager()
    {
        instance = this;
    }

    public UIResourceManager GetInstance()
    {
        if (instance == null)
        {
            return new UIResourceManager();
        }
        else
        {
            return instance;
        }
    }

    // -----------------------------------------------

    public Text MetalText;
    public Text GasText;
    public Text FoodText;

    public void UpdateMetalText(int amount)
    {
        MetalText.text = "Metals: " + amount.ToString();
    }

    public void UpdateGasText(int amount)
    {
        GasText.text = "Gases: " + amount.ToString();
    }

    public void UpdateFoodText(int amount)
    {
        FoodText.text = "Food: " + amount.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
