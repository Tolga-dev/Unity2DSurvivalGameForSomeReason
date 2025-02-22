using So.ItemsSo.WeaponsItem.Base;
using UnityEngine;

namespace So.ItemsSo.WeaponsItem
{
    [CreateAssetMenu(fileName = "ItemRangeWeapon", menuName = "So/ItemRangeWeapon", order = 0)]
    public class RangeWeaponSo : WeaponSo
    {
        [SerializeField] protected ConsumableItems.Base.Consumable ammunition = null;
        [SerializeField] private int ammoConsumption = 1;

        public override string GetFullDescription()
        {
            string fullDescription = $"{description}. It has a dps of {dps}.";
            fullDescription += $" It requires a {ammunition.DisplayName} to be shot.";
            return fullDescription;
        }

        public ConsumableItems.Base.Consumable GetAmmunition()
        {
            return ammunition;
        }
    }
}