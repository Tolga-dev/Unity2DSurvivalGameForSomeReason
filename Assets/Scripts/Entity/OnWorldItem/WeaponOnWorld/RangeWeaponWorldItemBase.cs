using System.Collections.Generic;
using System.Linq;
using Manager;
using So.ItemsSo.ConsumableItems;
using So.ItemsSo.WeaponsItem.Base;
using UI.Inventory;
using UnityEngine;

namespace Entity.OnWorldItem.WeaponOnWorld
{
    public class RangeWeaponWorldItemBase : WeaponWorldItemBase
    {
        [Header("Range Weapons Components")]
        [SerializeField] private DamagingConsumable ammoType;
        public Transform spawnPoint;
        
        private float _nextFireTime = 0f;
                                        
        public override void UseItem()
        {
            Shoot(spawnPoint);
        }
        private void Shoot(Transform firePoint)
        {
            if (Time.time < _nextFireTime) return;

            if (IsThereAmmo() == false)
            {
                Debug.Log("You Cant Shoot!");
                return;
            }
            
            var projectile = Instantiate(ammoType.onWorldItemPrefab, 
                firePoint.position, firePoint.rotation);
            var shoot = projectile.GetComponent<ShootWorldItemBase>();
            
            shoot.Initialize(firePoint.right);
            shoot.SetToGameMode();

            _nextFireTime = Time.time + ((WeaponSo)currentItem).fireRate; 
        }

        private bool IsThereAmmo()
        {
            var foundSlot = FindSlot();
            if(foundSlot == null)
                return false;
            foundSlot.HandleAmount();
            
            return true;
        }

        private InventorySlot FindSlot()
        {
            List<InventorySlot> slots = InventoryManager.hotbarSlots.Concat(InventoryManager.inventorySlots).ToList();

            foreach (var slot in slots)
            {
                if(slot.inventoryItem != null && slot.inventoryItem.currentItem == ammoType)
                    return slot;
            }

            return null;
        }
    }
}