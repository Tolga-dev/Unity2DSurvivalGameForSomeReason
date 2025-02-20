using System.Collections;
using System.Threading.Tasks;
using Core.StateMachine.Base;
using Entity.Npc.States.Abstract;
using So;
using UnityEngine;

namespace Entity.Npc.States.State
{
    public class EnemyAttackState : EnemyState
    {
        private Coroutine _waitForSecondsToCoolDown;
        public EnemyAttackState(StateControllerBase stateController) : base(stateController)
        {
        }
        public override void Enter()
        {
            EnemyBase.animationController.SetFloat(ActionType.Attack);
            if (_waitForSecondsToCoolDown == null)
            {
                Debug.Log("Attacked!");
                NavMeshAgent.SetDestination(EnemyBase.transform.position);
                _waitForSecondsToCoolDown = EnemyBase.StartCoroutine(WaitForSecondsToCoolDown());
                EnemyStateController.SetState<EnemyChaseState>();
            }
        }

        private IEnumerator WaitForSecondsToCoolDown()
        {
            yield return new WaitForSeconds(1);
            EnemyStateController.SetState<EnemyChaseState>();
            _waitForSecondsToCoolDown = null;
        }
    }
}