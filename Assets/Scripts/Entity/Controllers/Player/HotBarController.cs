using System;
using Entity.Controllers.Base;
using Entity.OnWorldItem.Base;
using Entity.Player;
using Manager;
using Manager.Base;
using So;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class HotBarController : ControllerBase
    {
        public int currentIndex = 0;
        public int oldIndex;
        private PlayerBase _playerBase;
        public UsableWorldItemBase currentItem;
        public Transform armTransform;

        public override void Start(ManagerBase playerBase)
        {
            base.Start(playerBase);
            _playerBase = (PlayerBase)playerBase;
        }
        public override void Update()
        {
            UpdateHotBarIndex();
        }
        
        private void UpdateHotBarIndex()
        {
            var slots = InventoryManager.hotbarSlots;
            if (slots == null || slots.Count == 0) return;

            var scroll = Input.GetAxis("Mouse ScrollWheel");

            switch (scroll)
            {
                case > 0f:
                    currentIndex = (currentIndex + 1) % slots.Count;
                    break;
                case < 0f:
                    currentIndex = (currentIndex - 1 + slots.Count) % slots.Count;
                    break;
                default:
                    return;
            }

            if (oldIndex != currentIndex)
            {
                oldIndex = currentIndex;
                CreateNewWorldItemAndDestroyOldOne();
            }
            
            SetHighLights();
        }
       
        private void CreateNewWorldItemAndDestroyOldOne()
        {
            if (currentItem != null)
            {
                Object.Destroy(currentItem.gameObject);
                currentItem = null;
            }

            var slots = InventoryManager.hotbarSlots;
            var currentSlot = slots[currentIndex];
            
            if(currentSlot.inventoryItem == null)
                return;
            
            var currentPrefab = currentSlot.inventoryItem.currentItem.onWorldItemPrefab;
            var created = Object.Instantiate(currentPrefab, armTransform.transform, true);
            created.transform.localPosition = Vector3.zero;
            created.transform.localRotation = Quaternion.identity;
            
            currentItem = created.GetComponent<UsableWorldItemBase>();
            currentItem.SetItemFromData(currentSlot.inventoryItem.currentItem, 0, InventoryManager);
            currentItem.SetToGameMode();
        }
        public void SetHighLights()
        {
            var slots = InventoryManager.hotbarSlots;
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].SetHighlight(i == currentIndex);
            }
        }

        public InventoryManager InventoryManager => _playerBase.gameManager.inventoryManager;

    }
}