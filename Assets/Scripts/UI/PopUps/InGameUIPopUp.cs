using Entity.Player;
using TMPro;
using UI.PopUps.Base;
using UnityEngine;

namespace UI.PopUps
{
    public class InGameUIPopUp : PopUpGameBase
    {
        [Header("Total Armor Protection")]
        public int totalArmorProtection;
        public TextMeshProUGUI totalArmorProtectionText;

        [Header("Pick Item Display Panel")]
        public Transform pickItemDisplayPanel;
        public PlayerBase playerBase;
        
        [Header("Health Lose Indicator")]
        public Canvas healthLoseIndicatorPrefab;
        public float duration = 0.5f;
        public float moveSpeed = 2f;  
        
        public bool CheckForPickItem()
        {
            if (playerBase.pickUpController.currentItem != null)
            {
                SetPickItemDisplayPanel(true);
                return true;
            }
            else
            {
                SetPickItemDisplayPanel(false);
                return false;
            }
        }

        private void SetPickItemDisplayPanel(bool b)
        {
            pickItemDisplayPanel.gameObject.SetActive(b);
        }
        public void SetUI()
        {
            totalArmorProtectionText.text = "Total Armor Protection " + totalArmorProtection;
        }
        public void SetTotalProtection(float val)
        {
            totalArmorProtection += (int)val; 
            SetUI();
        }
    }
}