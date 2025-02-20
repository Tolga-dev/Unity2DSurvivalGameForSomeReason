using Core.StateMachine.Abstract;
using Core.StateMachine.Base;

namespace Entity.Npc.States.Abstract
{
    public abstract class EnemyState : BaseState
    {
        protected EnemyState(StateControllerBase stateController) : base(stateController)
        {
        }
    }

}