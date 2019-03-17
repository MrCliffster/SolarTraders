using UnityEngine.UI;
using UnityEngine;
using System;

public class UIResourceManager : MonoBehaviour
{
    // ------------Singleton Stuff--------------------
    private static UIResourceManager instance;

    private UIResourceManager()
    {
        instance = this;
    }

    public static UIResourceManager GetInstance()
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

    public Text MetalROCText;
    public Text GasROCText;
    public Text FoodROCText;

    [SerializeField]
    private Color PositiveColor;
    [SerializeField]
    private Color NegativeColor;

    public void SetMetalText(int amount)
    {

        MetalText.text = "Metals: " + amount.ToString();
    }

    public void SetMetalROCText(int amount)
    {
        if (amount > 0)
        {
            MetalROCText.color = PositiveColor;
            MetalROCText.text = "+" + amount.ToString();
        }
        else
        {
            MetalROCText.color = NegativeColor;
            MetalROCText.text = amount.ToString();
        }
    }

    public void SetGasText(int amount)
    {
        GasText.text = "Gases: " + amount.ToString();
    }

    public void SetGasROCText(int amount)
    {
        if (amount > 0)
        {
            GasROCText.color = PositiveColor;
            GasROCText.text = "+" + amount.ToString();
        }
        else
        {
            GasROCText.color = NegativeColor;
            GasROCText.text = amount.ToString();
        }
    }

    public void SetFoodText(int amount)
    {
        FoodText.text = "Food: " + amount.ToString();
    }

    public void SetFoodROCText(int amount)
    {
        if (amount > 0)
        {
            FoodROCText.color = PositiveColor;
            FoodROCText.text = "+" + amount.ToString();
        }
        else
        {
            FoodROCText.color = NegativeColor;
            FoodROCText.text = amount.ToString();
        }
    }

    internal void SetAllROCs(int gasesRateOfChange, int metalsRateOfChange, int foodRateOfChange)
    {
        SetGasROCText(gasesRateOfChange);
        SetMetalROCText(metalsRateOfChange);
        SetFoodROCText(foodRateOfChange);
    }

    internal void SetAll(int gasses, int metals, int food)
    {
        SetGasText(gasses);
        SetMetalText(metals);
        SetFoodText(food);
    }
}
