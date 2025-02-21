using So.ItemsSo.Base;
using UnityEngine;

namespace So.ItemsSo.Weapons.Base
{
    [CreateAssetMenu(fileName = "ItemWeapon", menuName = "So/ItemWeapon", order = 0)]
    public class Weapon : Item
    {
        [SerializeField]
        protected int dps = 0;
        
        [SerializeField][TextArea]
        protected string attackDescription = string.Empty;
        
        public override string GetFullDescription()
        {
            string fullDescription = $"{description}. It has a dps of {dps}.";
            return fullDescription;
        }

        public override ItemTypes GetItemType()
        {
            return ItemTypes.Weapon;
        }

        public override string OnUseMessage()
        {
            return $"{attackDescription}, dealing {dps} dps.";
        }

        public int GetDps()
        {
            return dps;
        }
    }
}