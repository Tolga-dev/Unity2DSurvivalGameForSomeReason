using Core;
using Entity.Npc.Enemy;
using Entity.Player;
using Manager.Base;
using UnityEngine;

namespace Manager
{
    public class GameManager : ManagerBase
    {
        public PlayerManager playerManager;        
        public EnemyBase enemyBase;
    }
}