using System;
using Entity.OnWorldItem.Base;
using Manager;
using So.ItemsSo.ArmorItems;
using So.ItemsSo.Base;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public InventoryManager inventoryManager;
        
        public Image itemIcon;
        public TextMeshProUGUI itemAmountText;

        public Item currentItem;
        public int amount;

        public InventorySlot activeSlot;
        public InventorySlot oldActiveSlot;
        
        private InventoryItem _dividedInstance;
        public void SetItemData(Item item, InventorySlot parent)
        {
            if (parent != null)
            {
                activeSlot = parent;
                oldActiveSlot = activeSlot;
                activeSlot.inventoryItem = this;
            }

            currentItem = item;
            SetUI();
        }

        private void SetUI()
        {
            itemIcon.sprite = currentItem.Icon;
        }

        #region Begin Drag

        public void OnBeginDrag(PointerEventData eventData)
        {
            inventoryManager.trashSlot.gameObject.SetActive(true);
            if (amount > 1)
            {
                var dividedAmount = Mathf.CeilToInt(amount / 2f);  
                _dividedInstance = inventoryManager.CreateNewInstance(activeSlot, currentItem, dividedAmount);
                amount -= dividedAmount;
                
                SetAmount();
            }
            else
            {
                activeSlot.inventoryItem = null;
            }
            
            StartDrag();

            if (activeSlot.IsArmorOnCorrectPlace(currentItem))
            {
                inventoryManager.gameManager.playerBase.inGameUIPopUp.SetTotalProtection(-((Armor)currentItem).armorProtection);
            }
        }
 
        private void StartDrag()
        {
            SetIconTransparency(0.5f);
            itemIcon.raycastTarget = false;
            MoveToTopLayer();
        }

        private void MoveToTopLayer()
        {
            transform.SetParent(inventoryManager.inventoryPopUp.draggableItemsParent);
            transform.SetAsLastSibling();
        }

        #endregion

        #region OnDrag

        public void OnDrag(PointerEventData eventData)
        {
            FollowPointer(eventData);
        }

        private void FollowPointer(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        #endregion

        #region EndDrag

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryManager.trashSlot.gameObject.SetActive(false);
            
            if (activeSlot == null || activeSlot == oldActiveSlot)
            {
                Debug.Log("Dropped back to same slot");
                DestroyDividedInstance();
                EmptyNewPlaceDropped();
                activeSlot = oldActiveSlot;
            }
            else
            {
                Debug.Log("Dropped back to different slot");
                
                if (activeSlot.inventoryItem == null)
                {
                    EmptyNewPlaceDropped();
                }
                else if(activeSlot.inventoryItem.currentItem == currentItem)
                {
                    NewPlaceDroppedWithSameClass();
                }
                else
                {
                    NewPlaceDroppedDifferentClass();
                }
                oldActiveSlot = activeSlot;
            }
        }

        private void NewPlaceDroppedDifferentClass()
        {
            Debug.Log("NewPlaceDroppedDifferentClass");

            DestroyDividedInstance();
            activeSlot = oldActiveSlot;
            activeSlot.inventoryItem = this;
            SetDropped(oldActiveSlot);
            
            if (activeSlot.IsArmorOnCorrectPlace(currentItem))
            {
                inventoryManager.gameManager.playerBase.inGameUIPopUp.SetTotalProtection(((Armor)currentItem).armorProtection);
            }
            
        }
        private void NewPlaceDroppedWithSameClass()
        {
            Debug.Log("NewPlaceDroppedWithSameClass");

            var sum = activeSlot.inventoryItem.amount + amount;
            var maxStack = currentItem.MaxStackAbleAmount;

            if (maxStack >= sum)
            {
                activeSlot.inventoryItem.SetAmount(activeSlot.inventoryItem.amount + amount);
                Destroy(gameObject);
            }
            else
            {
                var leftAmount = sum - maxStack;
                activeSlot.inventoryItem.SetAmount(maxStack);
                amount = leftAmount;
                SetAmount();

                NewPlaceDroppedDifferentClass();
            }
        }

        private void EmptyNewPlaceDropped()
        {
            Debug.Log("EmptyNewPlaceDropped");

            if (activeSlot.IsSlotNone())
            {
                activeSlot.inventoryItem = this;
                SetDropped(activeSlot);
            }
            else
            {
                if (activeSlot.IsArmorOnCorrectPlace(currentItem))
                {
                    activeSlot.inventoryItem = this;
                    SetDropped(activeSlot);
                    
                    inventoryManager.gameManager.playerBase.inGameUIPopUp.SetTotalProtection(((Armor)currentItem).armorProtection);
                }
                else
                {
                    NewPlaceDroppedDifferentClass();
                }
            }
        }
        private void SetDropped(InventorySlot targetSlot)
        {
            transform.SetParent(targetSlot.transform);
            transform.localPosition = Vector3.zero;
            SetIconTransparency(1f);
            itemIcon.raycastTarget = true;
            _dividedInstance = null;
        } 
        private void SetIconTransparency(float alpha)
        {
            Color temp = itemIcon.color;
            temp.a = alpha;
            itemIcon.color = temp;
        }

        private void DestroyDividedInstance()
        {
            if (_dividedInstance != null)
            {
                Debug.Log("Remove Divided Instance");
                amount += _dividedInstance.amount;
                SetAmount();
                    
                Destroy(_dividedInstance.gameObject);
                _dividedInstance = null;
            }
        }
        #endregion

        public void SetAmount(int amountVal = 0)
        {
            if (amountVal != 0)
                amount = amountVal;

            itemAmountText.text = amount.ToString();
        }
    }
}
