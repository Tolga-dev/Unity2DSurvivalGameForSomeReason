namespace Core.StateMachine.Interface
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
    }
}