using Events;
using Interfaces;

namespace Enemies.BossStates
{
    public class BossDeathState : IState
    {
        public void Enter(EnemyStateMachine enemy)
        {
            GameEvents.onBossFightEndEvent?.Invoke();
        }

        public void Execute(EnemyStateMachine enemy = null)
        {
            
        }

        public void Exit()
        {
            
        }
    }
}