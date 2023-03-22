using Enemies;

namespace Interfaces
{
    public interface IState
    {
        void Enter(EnemyStateMachine enemy);
        void Execute(EnemyStateMachine enemy = null);
        void Exit();
    }
}