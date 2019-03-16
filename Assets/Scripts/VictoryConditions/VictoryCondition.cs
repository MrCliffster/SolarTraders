using UnityEngine;

namespace SolarTraders
{
    public abstract class VictoryCondition : MonoBehaviour
    {
        public abstract bool PlayerMeetsConditions(PlayerManager player);

        public abstract string GetDescription();
    }
}
