using Core.StateMachine.Abstract;
using Core.StateMachine.Base;
using Entity.Controllers.Npc.Enemy;
using Entity.Npc.Enemy;
using Entity.Npc.States.Controller;
using UnityEngine;
using UnityEngine.AI;

namespace Entity.Npc.States.Abstract
{
    public abstract class EnemyState : BaseState
    {
        protected readonly NavMeshAgent NavMeshAgent;
        protected readonly EnemyBase EnemyBase;
        protected readonly PlayerDetectorController PlayerDetectorController;
        protected readonly EnemyStateController EnemyStateController;

        protected EnemyState(StateControllerBase stateController) : base(stateController)
        {
            var state = ((EnemyStateController)stateController);
            EnemyBase =state.EnemyBase;
            EnemyStateController = EnemyBase.EnemyStateController;
            NavMeshAgent = EnemyBase.navMeshAgent;
            PlayerDetectorController = EnemyBase.playerDetectorController;
        }
        
    }

}