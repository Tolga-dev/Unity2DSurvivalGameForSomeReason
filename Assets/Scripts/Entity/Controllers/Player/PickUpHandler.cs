using System;
using Entity.Controllers.Base;
using Entity.OnWorldItem;
using Entity.OnWorldItem.Base;
using Entity.Player;
using Manager.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class PickUpController : ControllerBase
    {
        public WorldItemBase currentItem;
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
        private void HandlePickedItem(WorldItemBase worldItemBase)
        {
            var inventoryManager = _playerBase.gameManager.inventoryManager;
            
            var resAmount = inventoryManager.SpawnInventoryItem(worldItemBase.currentItem, worldItemBase.currentItemAmount);

            if(resAmount == 0)
                Object.Destroy(worldItemBase.gameObject);
            else
                worldItemBase.SetItemFromData(worldItemBase.currentItem, resAmount, inventoryManager);
        }
        
    }
}