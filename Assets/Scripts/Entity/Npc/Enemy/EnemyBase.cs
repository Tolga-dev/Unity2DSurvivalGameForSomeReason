using Entity.Controllers.Npc;
using Entity.Controllers.Npc.Enemy;
using Entity.Npc.States.Controller;
using Entity.Npc.States.State;
using Manager;
using Manager.Base;
using So;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Npc.Enemy
{
    public class EnemyBase : ManagerBase
    {
        public GameManager gameManager;
        public NavMeshAgent navMeshAgent;
        public PlayerDetectorController playerDetectorController;
        public AnimationController animationController;
        
        public EnemySo entitySo;
        private readonly EnemyStateController _enemyStateController = new EnemyStateController();
        
        protected override void Awake()
        {
        }
        
        public void Start()
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;

            animationController.Start(this);
            _enemyStateController.Starter(gameManager);
        }
        public void Update()
        {
            _enemyStateController.Update();
        }
        public void PlayerDetectedByIdleCollider()
        {
            _enemyStateController.SetState<EnemyChaseState>();
        }
        public void PlayerLostByChaseCollider()
        {
            _enemyStateController.SetState<EnemyIdleState>();
        }
        
        public EnemyStateController EnemyStateController => _enemyStateController; 
    }
}