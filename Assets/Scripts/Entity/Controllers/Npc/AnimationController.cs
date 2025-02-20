using System;
using Entity.Controllers.Base;
using Entity.Npc.Enemy;
using So;
using UnityEngine;

namespace Entity.Controllers.Npc
{
    [Serializable]
    public class AnimationController : ControllerBase
    {
        public Animator animator;

        public void SetFloat(ActionType actionType)
        {
            var id = EnemyBase.entitySo.GetAnimationIdFromActionType(actionType);
            animator.SetFloat(id.name, id.animationValue);
        }

        public EnemyBase EnemyBase => (EnemyBase)ManagerBase;
    }
}
