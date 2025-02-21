using System;
using Entity.Controllers.Base;
using Entity.Player;
using Manager.Base;
using UI.PopUps;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class InputController : ControllerBase
    {
        private PlayerBase _playerBase;
        private Vector2 _input = Vector2.zero;
        private InventoryPopUp _inventoryPopUp; 
        public override void Start(ManagerBase managerBase)
        {
            base.Start(managerBase);
            _playerBase = (PlayerBase) managerBase;
            _inventoryPopUp = _playerBase.gameManager.popUpController.GetPopUp<InventoryPopUp>();
        }
        
        public override void Update()
        {
            _input = GetInput();
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventoryPopUp.ToggleInventoryUI();
            }
        }

        private Vector2 GetInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        public Vector2 MovementInput => _input;
    }
}