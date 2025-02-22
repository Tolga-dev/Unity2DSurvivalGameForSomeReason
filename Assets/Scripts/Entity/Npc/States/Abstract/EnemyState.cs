using Core.StateMachine.Abstract;
using Core.StateMachine.Base;
using Entity.Controllers.Npc.Enemy;
using Entity.Npc.Enemy;
using Entity.Npc.States.Controller;
using So;
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
        protected readonly EnemySo EnemySo;

        protected EnemyState(StateControllerBase stateController) : base(stateController)
        {
            var state = ((EnemyStateController)stateController);
            EnemyBase =state.EnemyBase;
            EnemyStateController = EnemyBase.EnemyStateController;
            NavMeshAgent = EnemyBase.navMeshAgent;
            PlayerDetectorController = EnemyBase.playerDetectorController;
            EnemySo = (EnemySo)EnemyBase.entitySo;
        }

        protected void PlayFx(FxSoEnum fxSoEnum, Transform parentForPosition, Transform parent = null, bool durationDestroy = false)
        {
            EnemyBase.gameManager.fxManager.PlayFx(fxSoEnum, parentForPosition, parent, durationDestroy);
        }

        protected void SetFloat(ActionType actionType)
        {
            EnemyBase.animationController.SetFloat(actionType);

        }

    }

}