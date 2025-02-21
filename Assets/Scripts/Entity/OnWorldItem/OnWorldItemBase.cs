using System;
using Entity.Player;
using So.ItemsSo.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.OnWorldItem
{
    public class OnWorldItemBase : MonoBehaviour
    {
        public Image itemIcon;
        public Item currentItem;
        
        public TextMeshProUGUI itemAmount;
        public int currentItemAmount;
        
        public void SetItemFromData(Item item, int amount)
        {
            currentItem = item;

            itemIcon.sprite = item.Icon;
            
            itemAmount.text = amount.ToString();
            currentItemAmount = amount;
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
    }
}