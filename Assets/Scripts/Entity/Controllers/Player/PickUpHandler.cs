using System;
using Entity.Controllers.Base;
using Entity.OnWorldItem;
using Entity.Player;
using Manager.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class PickUpController : ControllerBase
    {
        public OnWorldItemBase currentItem;
        private PlayerBase _playerBase;
        
        public override void Start(ManagerBase managerBase)
        {
            base.Start(managerBase);
            _playerBase = (PlayerBase) managerBase;
        }
        
        public override void Update()
        {
            if (_playerBase.inGameUIPopUp.CheckForPickItem())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HandlePickedItem(currentItem);
                }
            }
        }
        private void HandlePickedItem(OnWorldItemBase onWorldItemBase)
        {
            var resAmount = _playerBase.gameManager.inventoryManager.SpawnInventoryItem(onWorldItemBase.currentItem, onWorldItemBase.currentItemAmount);

            if(resAmount == 0)
                Object.Destroy(onWorldItemBase.gameObject);
            else
                onWorldItemBase.SetItemFromData(onWorldItemBase.currentItem, resAmount);
        }
        
    }
}