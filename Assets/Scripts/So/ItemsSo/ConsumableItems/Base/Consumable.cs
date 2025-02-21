using So.ItemsSo.Base;
using UnityEngine;

namespace So.ItemsSo.Consumable.Base
{
    [CreateAssetMenu(fileName = "ItemConsumable", menuName = "So/Consumable", order = 0)]
    public abstract class Consumable : Item
    {
        [SerializeField] protected int amount;
        public override ItemTypes GetItemType()
        {
            return ItemTypes.Consumable;
        }
    }
}