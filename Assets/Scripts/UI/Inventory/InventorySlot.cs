using System.Collections;
using Manager;
using So.ItemsSo.ArmorItems;
using So.ItemsSo.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{
    public enum SlotTag { None, Head, Chest, Legs, Feet,OutSide}

    public class InventorySlot : MonoBehaviour, IDropHandler,IPointerEnterHandler,IPointerExitHandler, IPointerDownHandler
    {
        public InventoryManager inventoryManager;
        public InventoryItem inventoryItem;
        public SlotTag myTag;
        public Image slotImage;
        public void OnDrop(PointerEventData eventData)
        {
            var currentItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            currentItem.activeSlot = this;
            
            if (myTag == SlotTag.OutSide)
            {
                Debug.Log("Trash Slot");
                CreateARealWorldItemInstance(currentItem);
            }
        }

        public bool IsSlotNone()
        {
            return myTag is SlotTag.None or SlotTag.OutSide;
        }

        public bool IsArmorOnCorrectPlace(Item item)
        {
            if (item.GetItemType() != ItemTypes.Armor)
                return false;
            
            var armor = (Armor)item;
            var currentArmorType = armor.GetArmorType();
            switch (currentArmorType)
            {
                case SlotTag.Head:
                    return myTag == SlotTag.Head;
                case SlotTag.Chest:
                    return myTag == SlotTag.Chest;
                case SlotTag.Legs:
                    return myTag == SlotTag.Legs;
                case SlotTag.Feet:
                    return myTag == SlotTag.Feet;
            }
            return false;
        }
        private void CreateARealWorldItemInstance(InventoryItem currentItem)
        {
            inventoryManager.StartCoroutine(WaitForAFrame(currentItem));
        }

        private IEnumerator WaitForAFrame(InventoryItem currentItem)
        {
            yield return new WaitForEndOfFrame();
            currentItem.inventoryManager.gameManager.mapWorldGenerator.SpawnFromItem(currentItem.currentItem, amount: currentItem.amount);
            Destroy(currentItem.gameObject);
            inventoryItem = null;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (inventoryItem != null)
            {
                var descriptionPanelText = $"{inventoryItem.currentItem.name}: {inventoryItem.currentItem.GetFullDescription()}";
                var descriptionPanel = inventoryManager.inventoryPopUp.SetDescriptionPanel(descriptionPanelText,transform);
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(descriptionPanel);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerDown(eventData);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (inventoryItem != null)
            {
                inventoryManager.inventoryPopUp.SetPanel(false);
            }
        }

        public void HandleAmount()
        {
            if (inventoryItem.currentItem.GetItemType() != ItemTypes.Consumable)
                return;
            
            inventoryItem.amount--;
            inventoryItem.SetAmount();

            if (inventoryItem.amount <= 0)
            {
                Destroy(inventoryItem.gameObject);
                inventoryItem = null;
            }
        }

        public void SetHighlight(bool b)
        {
            slotImage.color = b ? Color.red : Color.white;
        }
    }
}