public interface IGameState<T>
{
    public void EnterState(T stateMachine);
    public void UpdateState(T stateMachine);
    public void ExitState(T stateMachine);

}