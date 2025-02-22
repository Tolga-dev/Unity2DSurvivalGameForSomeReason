using So.ItemsSo.Base;
using UnityEngine;

namespace So.ItemsSo.WeaponsItem.Base
{
    [CreateAssetMenu(fileName = "ItemWeapon", menuName = "So/ItemWeapon", order = 0)]
    public class WeaponSo : Item
    {
        [SerializeField]
        public int dps = 0;
        
        [SerializeField] public float fireRate = 0.5f; 
        
        public override string GetFullDescription()
        {
            string fullDescription = $"{description}. It has a dps of {dps}.";
            return fullDescription;
        }

        public override ItemTypes GetItemType()
        {
            return ItemTypes.Weapon;
        }
    }
}