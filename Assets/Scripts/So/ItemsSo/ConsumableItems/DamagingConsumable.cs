using UnityEngine;

namespace So.ItemsSo.ConsumableItems
{
    [CreateAssetMenu(fileName = "ItemDamagingConsumable", menuName = "So/DamagingConsumable", order = 0)]
    public class DamagingConsumable : Base.Consumable
    {
        public float speed;
    }
}