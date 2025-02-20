using Entity.Npc.States.Controller;
using Manager;
using UnityEngine;

namespace Entity.Npc.Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public GameManager gameManager;
        private EnemyStateController _enemyStateController;
        
        
        public void Start()
        {
            _enemyStateController = new EnemyStateController();
            _enemyStateController.Starter(gameManager);
        }

        public void Update()
        {
            _enemyStateController.Update();
        }
        
    }
}