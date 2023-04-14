using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossInterruptedState : IState
    {
        private BossEnemyStateMachine _enemy;

        private float _interruptWaitTime;
        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;

            if (_enemy != null)
            {
                _interruptWaitTime = _enemy.enemyData.interruptTime;

                _enemy.animator.CrossFade("Interrupted", 0);
            }
        }

        public void Execute(EnemyStateMachine enemy)
        {

            if (_interruptWaitTime > 0)
            {
                _interruptWaitTime -= Time.deltaTime;
                //Stop any movement
                enemy.Rb.velocity = Vector2.zero;
            }
            else
            {
                //Finish Animation & Return to Aim State
                enemy.ChangeState(new BossAimState());
            }

        }

        public void Exit()
        {
        }
    }
}
