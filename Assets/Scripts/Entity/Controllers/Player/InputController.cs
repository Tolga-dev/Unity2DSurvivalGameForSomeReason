using System;
using Entity.Controllers.Base;
using Entity.Player;
using Manager;
using Manager.Base;
using UI.PopUps;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Entity.Controllers.Player
{
    [Serializable]
    public class InputController : ControllerBase
    {
        private InventoryPopUp _inventoryPopUp;
        
        private PlayerBase _playerBase;
        private Vector2 _input = Vector2.zero;

        [Header("Controllers")]
        public ArmController armController;
        public HotBarController hotBarController;
        public FireController fireController;

        public override void Start(ManagerBase managerBase)
        {
            base.Start(managerBase);
            armController.Start(managerBase);
            hotBarController.Start(managerBase);
            fireController.Start(managerBase);
            
            _playerBase = (PlayerBase) managerBase;
            _inventoryPopUp = _playerBase.gameManager.popUpController.GetPopUp<InventoryPopUp>();
            
            hotBarController.SetHighLights();
        }
        
        public override void Update()
        {
            _input = GetInput();
            
            hotBarController.Update();
            
            CheckForUIPanelInput();
            
            armController.Update();
            
            if (EventSystem.current.IsPointerOverGameObject()) return;

            fireController.Update();
        }

        private void CheckForUIPanelInput()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                hotBarController.oldIndex = -1;
                _inventoryPopUp.ToggleInventoryUI();
            }
        }
        
        private Vector2 GetInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        
        public Vector2 MovementInput => _input;
        public InventoryManager InventoryManager => _playerBase.gameManager.inventoryManager;
    }
}