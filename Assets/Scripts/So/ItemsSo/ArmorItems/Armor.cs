using So.ItemsSo.Base;
using UI.Inventory;
using UnityEngine;

namespace So.ItemsSo.ArmorItems
{
    [CreateAssetMenu(fileName = "Armor", menuName = "So/Armor", order = 0)]
    public class Armor : Item
    {
        public float armorDurability;
        public float armorProtection;
        
        [SerializeField][TextArea]
        protected string attackDescription = string.Empty;

        public SlotTag armorTypeTag;
        public override ItemTypes GetItemType()
        {
            return ItemTypes.Armor;
        }
        public SlotTag GetArmorType()
        {
            return armorTypeTag;
        }
    }
}