using System.Collections;
using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using So;
using UnityEngine;
using UnityEngine.AI;


namespace Entity.Npc.States.State
{
    public class EnemyPatrolState : EnemyState
    {
        private Coroutine _waitForSecondsToCoolDown;
        private readonly float _patrolRadius = 10f;
        private readonly float _minDistanceToTarget = 0.5f;
        private readonly float _waitTime = 1f;

        public EnemyPatrolState(StateControllerBase stateController) : base(stateController)
        {
        }

        public override void Enter()
        {
            EnemyBase.animationController.SetFloat(ActionType.Patrol);
            PlayerDetectorController.SetIdleCollider(true);
            SetNewPatrolTarget();
        }

        public override void Update()
        {
            if (CanSelectNewTarget() && _waitForSecondsToCoolDown == null)
            {
                _waitForSecondsToCoolDown = EnemyBase.StartCoroutine(WaitAndSetNewTarget());
            }
        }

        public override void Exit()
        {
            if (_waitForSecondsToCoolDown != null)
            {
                EnemyBase.StopCoroutine(_waitForSecondsToCoolDown);
                _waitForSecondsToCoolDown = null;
            }
            PlayerDetectorController.SetIdleCollider(false);
        }

        private void SetNewPatrolTarget()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _patrolRadius;
            randomDirection += EnemyBase.transform.position;

            if (NavMesh.SamplePosition(randomDirection, out var hit, _patrolRadius, NavMesh.AllAreas))
            {
                NavMeshAgent.SetDestination(hit.position);
            }
        }

        private bool CanSelectNewTarget()
        {
            return !NavMeshAgent.pathPending && NavMeshAgent.remainingDistance < _minDistanceToTarget;
        }

        private IEnumerator WaitAndSetNewTarget()
        {
            yield return new WaitForSeconds(_waitTime);
            _waitForSecondsToCoolDown = null; // Reset the cooldown
            SetNewPatrolTarget();
        }

    }
}