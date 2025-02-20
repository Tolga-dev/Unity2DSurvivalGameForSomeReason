using System;
using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using Entity.Npc.States.State;
using Manager;

namespace Entity.Npc.States.Controller
{
    [Serializable]
    public class EnemyStateController : StateControllerBase
    {
        private EnemyChaseState _chaseState;
        private EnemyIdleState _idleState;
        private EnemyPatrolState _patrolState;
        private EnemyAttackState _attackState;
         
        public virtual void Starter(GameManager gameManager)
        {
            GameManager = gameManager;
            
            _chaseState = new EnemyChaseState(this);
            _idleState = new EnemyIdleState(this);
            _patrolState = new EnemyPatrolState(this);
            _attackState = new EnemyAttackState(this);
            
            AddState(_chaseState);
            AddState(_attackState);
            AddState(_idleState);
            AddState(_patrolState);
            
            SetState<EnemyIdleState>();
        }
        public GameManager GameManager { get; set; }
    }
}