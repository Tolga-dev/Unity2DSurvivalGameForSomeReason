using System;
using System.Collections.Generic;
using System.Linq;
using Entity.Player;
using So.ItemsSo.Base;
using TMPro;
using UI.Inventory;
using UI.PopUps;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Manager
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] List<InventorySlot> inventorySlots = new List<InventorySlot>();
        [SerializeField] List<InventorySlot> hotbarSlots = new List<InventorySlot>();
        [SerializeField] List<InventorySlot> equipmentSlots = new List<InventorySlot>();

        [Header("Item List")] 
        [SerializeField] public List<Item> items = new List<Item>();
        public GameManager gameManager;

        public InventoryPopUp inventoryPopUp;
        private void Awake()
        {
            inventoryPopUp = gameManager.popUpController.GetPopUp<InventoryPopUp>();
        }
        
        public int SpawnInventoryItem(Item item, int amount)
        {
            var result = CanAddAllItemsToInv(item,amount);
            if (result == 0)
                return 0;

            result = AddToEmptySlots(item, amount);
            if (result != 0)
            {
                Debug.Log("Inv Full!");
                return result;
            }
            return 0;
        }
        private int AddToEmptySlots(Item inventoryItem, int amount)
        {
            var slots = inventorySlots.Concat(hotbarSlots).ToList();
            var leftItem = SearchForEmptyItem(slots, inventoryItem, amount);
            return leftItem;
        }
        private int SearchForEmptyItem(List<InventorySlot> slots, Item inventoryItem, int amount)
        {
            foreach (var inventorySlot in slots)
            {
                if (amount <= 0)
                    break;

                if (inventorySlot.inventoryItem != null) 
                    continue; 

                int amountToAdd = Mathf.Min(inventoryItem.MaxStackAbleAmount, amount);
                var createdItem = CreateNewInstance(inventorySlot,inventoryItem, amountToAdd);

                inventorySlot.inventoryItem = createdItem;
                amount -= amountToAdd;
            }

            return amount; // Return remaining amount if inventory is full
        }
        public InventoryItem CreateNewInstance(InventorySlot inventorySlot, Item item, int amountToAdd)
        {
            var createdItem= Instantiate(inventoryPopUp.inventoryUI.itemPrefab, inventorySlot.transform);
            createdItem.SetItemData(item, inventorySlot);
                
            createdItem.amount = amountToAdd;
            createdItem.SetAmount();
            
            createdItem.inventoryManager = this;
            
            return createdItem;
        }
        
        private int CanAddAllItemsToInv(Item inventoryItem, int amount)
        {
            var slots = inventorySlots.Concat(hotbarSlots).ToList();
            var leftItem = SearchForSameItem(slots, inventoryItem, amount);
            return leftItem;
        }
       
        private int SearchForSameItem(List<InventorySlot> slots, Item inventoryItem, int amount)
        {
            foreach (var inventorySlot in slots)
            {
                if (amount <= 0)
                    break;

                var invItem = inventorySlot.inventoryItem;
        
                if (invItem == null || invItem.currentItem != inventoryItem)
                    continue;

                int spaceAvailable = invItem.currentItem.MaxStackAbleAmount - invItem.amount;

                if (spaceAvailable <= 0)
                    continue; 

                int amountToAdd = Mathf.Min(spaceAvailable, amount);
                
                invItem.amount += amountToAdd;
                inventorySlot.inventoryItem.SetAmount();
                
                amount -= amountToAdd;
            }
            return amount;
        }
        public Item PickRandomItem()
        {
            int random = Random.Range(0, items.Count);
            return items[random];
        }
    }
    
    [Serializable]
    public class InventoryUI
    {
        [Header("Debug")]
        [SerializeField] public Button giveItemBtn;
        [SerializeField] public InventoryItem itemPrefab;

        [Header("Hover Item")]
        public RectTransform descriptionPanel;
        public TextMeshProUGUI descriptionPanelText;

        [Header("Hot Transform Panels")]
        public RectTransform hotPlace;
        public RectTransform playerPanelHotPlace;
        public RectTransform inventoryHotPlacePanel;
        
        [Header("Transform Panels")]
        [SerializeField] public GameObject inventoryUI;
        [SerializeField] public GameObject playerUI;
    }
}
