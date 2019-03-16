using UnityEngine;

namespace SolarTraders
{
    public class ColonisedAllPlanets : VictoryCondition
    {
        public override string GetDescription()
        {
            return "Colonise all planets!";
        }

        public override bool PlayerMeetsConditions(PlayerManager player)
        {
            if (player.colonisedPlanets.Count ==  (Universe.Instance.Systems[0].Bodies.Count - 1))
            {
                // Win
                return true;
            }
            return false;
        }

    }
}