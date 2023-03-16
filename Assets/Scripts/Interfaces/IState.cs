public interface IState
{
    public void EnterState(IState state);
    public void UpdateState(IState state);
    public void ExitState(IState state);
}