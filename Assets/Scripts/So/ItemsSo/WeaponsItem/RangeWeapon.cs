using So.ItemsSo.Weapons.Base;
using UnityEngine;

namespace So.ItemsSo.Weapons
{
    [CreateAssetMenu(fileName = "ItemRangeWeapon", menuName = "So/ItemRangeWeapon", order = 0)]
    public class RangeWeapon : Weapon
    {
        [SerializeField] protected Consumable.Base.Consumable ammunition = null;

        public override string GetFullDescription()
        {
            string fullDescription = $"{description}. It has a dps of {dps}.";
            fullDescription += $" It requires a {ammunition.DisplayName} to be shot.";
            return fullDescription;
        }

        public Consumable.Base.Consumable GetAmmunition()
        {
            return ammunition;
        }
    }
}