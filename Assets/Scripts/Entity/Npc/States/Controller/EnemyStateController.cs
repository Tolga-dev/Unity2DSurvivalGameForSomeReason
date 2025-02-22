using System;
using Core.StateMachine.Base;
using Entity.Npc.Enemy;
using Entity.Npc.States.Abstract;
using Entity.Npc.States.State;
using Manager;

namespace Entity.Npc.States.Controller
{
    public class EnemyStateController : StateControllerBase
    {
        private EnemyChaseState _chaseState;
        private EnemyIdleState _idleState;
        private EnemyPatrolState _patrolState;
        private EnemyAttackState _attackState;
        private EnemyDeadState _enemyDeadState;
        
        public void Starter(GameManager gameManager)
        {
            GameManager = gameManager;
            EnemyBase = gameManager.enemyBase;
            
            _chaseState = new EnemyChaseState(this);
            _idleState = new EnemyIdleState(this);
            _patrolState = new EnemyPatrolState(this);
            _attackState = new EnemyAttackState(this);
            _enemyDeadState = new EnemyDeadState(this);
            
            AddState(_chaseState);
            AddState(_attackState);
            AddState(_idleState);
            AddState(_patrolState);
            AddState(_enemyDeadState);
            
            SetState<EnemyIdleState>();
            
        }
        public void SetToAttackState()
        {
            SetState<EnemyAttackState>();
        }
        
        public GameManager GameManager { get; set; }
        public EnemyBase EnemyBase { get; set; }
    }
}