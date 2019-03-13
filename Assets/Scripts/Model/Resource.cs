using UnityEngine;
namespace SolarTraders {

    public class ResourceStockpile
    {
        public enum ResourceType { Metals, Gasses, Food};
        public int metals;
        public int gasses;
        public int food;

        // Add an amount of a resource as per:
        // 0 = all
        // 1 = metals
        // 2 = gasses 
        // 3 = food
        public void AddResource(int resource, int amount)
        {
            switch (resource)
            {
                case 0:
                    metals += amount;
                    gasses += amount;
                    food += amount;
                    break;
                case 1:
                    metals += amount;
                    break;
                case 2:
                    gasses += amount;
                    break;
                case 3:
                    food += amount;
                    break;
                default:
                    Debug.LogError("Invalid Resource added!");
                    break;
            }
        }

        // Add an amount of a resource as per:
        // 0 = all
        // 1 = metals
        // 2 = gasses 
        // 3 = food
        public void AddResource(ResourceType resource, int amount)
        {
            switch (resource)
            {
                case ResourceType.Metals:
                    metals += amount;
                    break;
                case ResourceType.Gasses:
                    gasses += amount;
                    break;
                case ResourceType.Food:
                    food += amount;
                    break;
                default:
                    Debug.LogError("Invalid Resource added!");
                    break;
            }
        }

        // Subtract an amount of a resource as per:
        // 0 = all
        // 1 = metals
        // 2 = gasses 
        // 3 = food
        public void SubtractResource(int resource, int amount)
        {
            amount = -amount;
            switch (resource)
            {
                case 0:
                    metals += amount;
                    gasses += amount;
                    food += amount;
                    break;
                case 1:
                    metals += amount;
                    break;
                case 2:
                    gasses += amount;
                    break;
                case 3:
                    food += amount;
                    break;
                default:
                    Debug.LogError("Invalid Resource added!");
                    break;
            }
        }

    }
}