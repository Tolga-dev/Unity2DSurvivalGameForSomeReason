using UnityEngine;

namespace So.ItemsSo.Consumable
{
    [CreateAssetMenu(fileName = "ItemDamagingConsumable", menuName = "So/DamagingConsumable", order = 0)]
    public class DamagingConsumable : Base.Consumable
    {
        public override string OnUseMessage()
        {
            return $"You deal {amount} damage points.";
        }
    }
}