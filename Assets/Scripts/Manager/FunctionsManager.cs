using Entity.Player;
using Manager.Base;

namespace Manager
{
    public class FunctionsManager : ManagerBase
    {
        public void SetHealth(int val)
        {
            var player = GetPlayer();
            player.inGameUIPopUp.SetHealth(val);
        }
        public void SetHunger(int val)
        {            
            var player = GetPlayer();
            player.inGameUIPopUp.SetHunger(val);
        }

        private PlayerBase GetPlayer()
        {
            return FindObjectOfType<PlayerBase>();
        }
    }
}