using System.Collections;
using Manager;
using So.ItemsSo.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.OnWorldItem.Base
{
    public class UsableWorldItemBase : WorldItemBase
    {
        [Header("Item Canvas Components")]
        public CircleCollider2D itemCollider;
        public Transform itemTransform;
        
        [Header("On Action Components")]
        public BoxCollider2D onActionCollider;
        public Transform onActionTransform;
        
        protected Vector3 StartPosition;
        protected Coroutine CurrentCoroutine;
        
        public override void SetItemFromData(Item item, int amount, InventoryManager inventoryManager)
        {
            base.SetItemFromData(item, amount, inventoryManager);
            SetToItemMode();
        }

        public void SetToItemMode()
        {
            SetColliderToItemMode(true);
        }
        public void SetToGameMode()
        {
            SetColliderToItemMode(false);
        }

        private void SetColliderToItemMode(bool b)
        {
            if(itemCollider != null)
                itemCollider.enabled = b;
            if(onActionCollider != null)
                onActionCollider.enabled = !b;
            
            if(itemTransform != null)
                itemTransform.gameObject.SetActive(b);
            if(onActionTransform != null)
                onActionTransform.gameObject.SetActive(!b);
        }
        
        public override void UseItem()
        {
            if (CurrentCoroutine == null)
            {
                CurrentCoroutine = StartCoroutine(UseItemWithAnimation());
            }
        }

        private IEnumerator UseItemWithAnimation()
        {
            yield return MoveTo(StartPosition + Vector3.up * 2, 20);
            yield return MoveTo(StartPosition, 20);

            yield return new WaitForSeconds(1);
            CurrentCoroutine = null;
            
            OnUseItemEvent();
            Destroy(gameObject);
        }

        protected void OnUseItemEvent()
        {
            ManagerInventory();
            currentItem.onUseEvent?.Invoke();
            Debug.Log(currentItem.GetFullDescription());
        }
        
        private void ManagerInventory()
        {
            var inputSystem = InventoryManager.gameManager.playerBase.inputController;
            var slots = InventoryManager.hotbarSlots;
            var currentSlot = slots[inputSystem.hotBarController.currentIndex];

            currentSlot.HandleAmount();
        }
        
        protected IEnumerator MoveTo(Vector3 target, float speed)
        {
            while (Vector3.Distance(transform.localPosition, target) > 0.01f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speed * Time.deltaTime);
                yield return null;
            }
        }
        
      
        

    }
    
}