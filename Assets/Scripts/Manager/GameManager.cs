using Entity.Npc.Enemy;
using Entity.Player;
using Fx;
using Manager.Base;
using UI.PopUps.Controller;

namespace Manager
{
    public class GameManager : ManagerBase
    {
        public FxManager fxManager;
        public InventoryManager inventoryManager;
        public MapWorldManager mapWorldGenerator;

        public PlayerBase playerBase;        
        public EnemyBase enemyBase;

        public PopUpController popUpController;

        public EnemyBase enemyPrefab;

    }
}