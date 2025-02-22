using System;
using Entity.Player;
using Manager;
using So.ItemsSo.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.OnWorldItem.Base
{
    public class WorldItemBase : MonoBehaviour
    {
        [Header("Item Canvas Components")]
        public Image itemIcon;
        public Item currentItem;
        
        public TextMeshProUGUI itemAmount;
        public int currentItemAmount;

        public Image onActionIcon;

        protected InventoryManager InventoryManager;
        public virtual void SetItemFromData(Item item, int amount, InventoryManager inventoryManager)
        {
            InventoryManager = inventoryManager;
            
            currentItem = item;

            itemIcon.sprite = item.Icon;
            
            itemAmount.text = amount.ToString();
            currentItemAmount = amount;

            SetActionFromData();
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerBase = other.GetComponent<PlayerBase>();
                playerBase.pickUpController.currentItem = this;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var playerBase = other.GetComponent<PlayerBase>();
                playerBase.pickUpController.currentItem = null;
            }
        }

        public virtual void UseItem()
        {
            
        }
        protected virtual void SetActionFromData()
        {
            onActionIcon.sprite = currentItem.Icon;
        }
    }
}