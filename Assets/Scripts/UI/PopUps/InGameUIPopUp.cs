using Entity.Player;
using TMPro;
using UI.PopUps.Base;
using UnityEngine;

namespace UI.PopUps
{
    public class InGameUIPopUp : PopUpGameBase
    {
        public Transform pickItemDisplayPanel;

        public int health;
        public int hunger;
        public int totalArmorProtection;
		
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI hungerText;
        public TextMeshProUGUI totalArmorProtectionText;

        public PlayerBase playerBase;
        
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
        public void SetPickItemDisplayPanel(bool b)
        {
            pickItemDisplayPanel.gameObject.SetActive(b);
        }
        public void SetUI()
        {
            healthText.text ="Health " + health;
            hungerText.text = "Hunger " + hunger;
            totalArmorProtectionText.text = "Total Armor Protection " + totalArmorProtection;
        }
		
        public void SetHealth(int val)
        {
            health += val; 
            SetUI();
        }
        public void SetTotalProtection(float val)
        {
            totalArmorProtection += (int)val; 
            SetUI();
        }
        public void SetHunger(int val)
        {            
            hunger += val; 
            SetUI();
        }
    }
}