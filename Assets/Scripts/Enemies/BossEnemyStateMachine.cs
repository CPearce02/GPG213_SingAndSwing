using Enemies.BossStates;

namespace Enemies
{
    public class BossEnemyStateMachine : EnemyStateMachine
    {
        
        public override void Start()
        {
            ChangeState(new BossIdleState());
        }
        
    }
}
