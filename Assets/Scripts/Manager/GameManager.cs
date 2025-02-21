using Entity.Npc.Enemy;
using Entity.Player;
using Manager.Base;
using UI.PopUps.Controller;

namespace Manager
{
    public class GameManager : ManagerBase
    {
        public InventoryManager inventoryManager;
        public MapWorldManager mapWorldGenerator;

        public PlayerBase playerBase;        
        public EnemyBase enemyBase;

        public PopUpController popUpController;
        
    }
}