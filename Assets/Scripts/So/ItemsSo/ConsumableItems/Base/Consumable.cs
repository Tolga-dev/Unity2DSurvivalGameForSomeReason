using So.ItemsSo.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace So.ItemsSo.ConsumableItems.Base
{
    [CreateAssetMenu(fileName = "ItemConsumable", menuName = "So/Consumable", order = 0)]
    public abstract class Consumable : Item
    {
        [SerializeField] public int effectAmount;
        public override ItemTypes GetItemType()
        {
            return ItemTypes.Consumable;
        }
    }
}