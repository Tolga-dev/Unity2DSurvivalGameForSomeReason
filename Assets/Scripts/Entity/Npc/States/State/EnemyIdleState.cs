using System.Collections;
using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using So;
using UnityEngine;

namespace Entity.Npc.States.State
{
    public class EnemyIdleState : EnemyState
    {
        private Coroutine _idleCoroutine;
        public EnemyIdleState(StateControllerBase stateController) : base(stateController)
        {
        }

        public override void Enter()
        {
            SetFloat(ActionType.Idle);
            PlayerDetectorController.SetIdleCollider(true);
            NavMeshAgent.SetDestination(EnemyBase.transform.position);
            
            _idleCoroutine ??= EnemyBase.StartCoroutine(WaitForPatrol());
        }

        private IEnumerator WaitForPatrol()
        {
            yield return new WaitForSeconds(1f);
            EnemyStateController.SetState<EnemyPatrolState>();
            _idleCoroutine = null;
        }
        public override void Exit()
        {
            if (_idleCoroutine != null)
            {
                EnemyBase.StopCoroutine(_idleCoroutine);
                _idleCoroutine = null;
            }
            
            PlayerDetectorController.SetIdleCollider(false);
        }
    }
}