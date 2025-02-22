using System.Collections;
using Entity.Base;
using Entity.Controllers.Npc.Enemy;
using Entity.Npc.States.Controller;
using Entity.Npc.States.State;
using Manager;
using So;
using UI.PopUps;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Npc.Enemy
{
    public class EnemyBase : EntityBase
    {
        [Header("Enemy")]
        public NavMeshAgent navMeshAgent;
        public PlayerDetectorController playerDetectorController;
        
        private readonly EnemyStateController _enemyStateController = new();
        protected override void Awake()
        {

        }
        
        protected override void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            inGameUIPopUp = FindObjectOfType<InGameUIPopUp>();
            gameManager.enemyBase = this;
            
            base.Start();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;

            _enemyStateController.Starter(gameManager);
        }
        
        public void Update()
        {
            _enemyStateController.Update();
        }
        
        protected override void EntityDeadAction()
        {
            EnemyStateController.SetState<EnemyDeadState>();
        }
        public EnemyStateController EnemyStateController => _enemyStateController;
    }
}