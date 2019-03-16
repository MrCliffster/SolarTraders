using UnityEngine;
using SolarTraders;

namespace SolarTraders
{
    public class AccumulateResources : VictoryCondition
    {
        public int goalAmount = 200;

        public override bool PlayerMeetsConditions(PlayerManager player) 
        {
            return player.resPool.food >= goalAmount && player.resPool.gasses >= goalAmount && player.resPool.metals >= goalAmount;
        }

        public override string GetDescription() 
        {
            return "Getting a certain amount of all resources!";
        }
    }
}
