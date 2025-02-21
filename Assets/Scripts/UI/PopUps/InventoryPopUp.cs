using Manager;
using UI.PopUps.Base;
using UnityEngine;

namespace UI.PopUps
{
    public class InventoryPopUp : PopUpGameBase
    {
        public InventoryUI inventoryUI;
        
        private void Awake()
        {
            inventoryUI.giveItemBtn.onClick.AddListener(() =>
            {
                InventoryManager.SpawnInventoryItem(InventoryManager.PickRandomItem(), 3);
            });
        }

        private void SetHotPanelPlace(RectTransform parent)
        {
            var hotPlace = inventoryUI.hotPlace;
            hotPlace.SetParent(parent, false);
            hotPlace.localPosition = Vector3.zero;
            hotPlace.anchoredPosition = Vector2.zero;
        }
        
        public void ToggleInventoryUI()
        {
            var playerUI = inventoryUI.playerUI;
            var uiManager = inventoryUI;
            bool isInventoryActive = inventoryUI.inventoryUI.activeSelf;

            inventoryUI.inventoryUI.SetActive(!isInventoryActive);
            playerUI.SetActive(isInventoryActive);

            SetHotPanelPlace(isInventoryActive ? uiManager.playerPanelHotPlace : uiManager.inventoryHotPlacePanel);
        }
        public RectTransform SetDescriptionPanel(string description)
        {
            var descriptionPanelText = inventoryUI.descriptionPanelText;
            var descriptionPanel = inventoryUI.descriptionPanel;
            descriptionPanel.transform.position = transform.position;

            descriptionPanelText.text = description;

            SetPanel(true);
            return descriptionPanel;
        }
        
        public void SetPanel(bool b)
        {
            var descriptionPanel = inventoryUI.descriptionPanel;
            descriptionPanel.gameObject.SetActive(b);
        }
        private GameManager GameManager => (GameManager)managerBase;
        private InventoryManager InventoryManager => GameManager.inventoryManager;

    }
}