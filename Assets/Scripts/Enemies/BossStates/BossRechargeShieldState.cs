using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossRechargeShieldState : IState
    {
        private BossEnemyStateMachine _enemy;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            if (_enemy == null) return;
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Animation

            //When finish aninamtion - Turn on sheild 
            _enemy.GetComponent<Enemy>().SetCanBeDestroyed(false);
            enemy.ChangeState(new BossAimState());
        }

        public void Exit()
        {
            
        }
    }
}