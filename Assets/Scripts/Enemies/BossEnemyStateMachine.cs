using Enemies.BossStates;
using UnityEngine;

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
