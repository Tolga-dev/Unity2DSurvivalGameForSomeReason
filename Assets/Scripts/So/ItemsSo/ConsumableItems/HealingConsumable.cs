using UnityEngine;

namespace So.ItemsSo.Consumable
{
    [CreateAssetMenu(fileName = "ItemHealingConsumable", menuName = "So/HealingConsumable", order = 0)]
    public class HealingConsumable : Base.Consumable
    { 
        public override string OnUseMessage()
        {
            return $"You heal yourself {amount} hp.";
        }
    }
}
