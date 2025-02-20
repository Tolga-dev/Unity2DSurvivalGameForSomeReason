using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using So;
using UnityEngine;

namespace Entity.Npc.States.State
{
    public class EnemyChaseState : EnemyState
    {
        private readonly float _attackRange = 2.0f;
        public EnemyChaseState(StateControllerBase stateController) : base(stateController)
        {
            
        }

        public override void Enter()
        {
            EnemyBase.animationController.SetFloat(ActionType.Chase);
            PlayerDetectorController.SetIdleCollider(false);
            NavMeshAgent.speed *= 1.5f; // set from so 
        }

        public override void Update()
        {
            if (PlayerDetectorController.IsPlayerClose(_attackRange))
                EnemyBase.EnemyStateController.SetToAttackState();
            else
                NavMeshAgent.SetDestination(PlayerDetectorController.PlayerPosition);
        }
        
        public override void Exit()
        {
            PlayerDetectorController.SetIdleCollider(true);
            NavMeshAgent.speed /= 1.5f;
        }
    }
}