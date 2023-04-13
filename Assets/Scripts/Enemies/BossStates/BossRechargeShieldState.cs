using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossRechargeShieldState : IState
    {
        private BossEnemyStateMachine _enemy;
        private Enemy _enemyComponent;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            _enemyComponent = _enemy.GetComponent<Enemy>();
            if (_enemy == null) return;
            _enemy.animator.CrossFade("Recharge", 0);
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Animation

            //When finish aninamtion - Turn on sheild 
            _enemyComponent.SetCanBeDestroyed(false);
            enemy.ChangeState(new BossAimState());
        }

        public void Exit()
        {

        }
    }
}