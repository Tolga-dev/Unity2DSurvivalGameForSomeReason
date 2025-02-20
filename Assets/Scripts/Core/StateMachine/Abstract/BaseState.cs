using Core.StateMachine.Base;
using Core.StateMachine.Interface;
using Entity.Npc;

namespace Core.StateMachine.Abstract
{
    public abstract class BaseState : IState
    {
        protected StateControllerBase StateControllerBase;

        protected BaseState(StateControllerBase stateController)
        {
            StateControllerBase = stateController;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }
    }
}