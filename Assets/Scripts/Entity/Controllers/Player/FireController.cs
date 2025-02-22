using System;
using Entity.Controllers.Base;
using Entity.Player;
using Manager.Base;
using So;
using UnityEngine;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class FireController : ControllerBase
    {
        private PlayerBase _playerBase;

        public override void Start(ManagerBase playerBase)
        {
            base.Start(playerBase);
            _playerBase = (PlayerBase)playerBase;
        }
        
        public override void Update()
        {
            UseCurrentItem();
        }
        
        private void UseCurrentItem()
        {
            if (Input.GetMouseButton(1))
            {
                Debug.Log("fire button");
                var currentItem = _playerBase.inputController.hotBarController.currentItem;
                if (currentItem != null)
                {
                    currentItem?.UseItem(); 
                    _playerBase.animationController.SetFloat(ActionType.Attack);
                }
            }
        }
    }
}