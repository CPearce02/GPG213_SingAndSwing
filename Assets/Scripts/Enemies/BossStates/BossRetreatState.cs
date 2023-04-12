using Enemies.EnemyStates;
using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossRetreatState : IState
    {
        private BossEnemyStateMachine _enemy;
        float _retreatTime;

        public void Enter(EnemyStateMachine enemy)
        {
            _enemy = enemy as BossEnemyStateMachine;
            if (_enemy == null) return;
            _retreatTime = enemy.enemyData.retreatTime;
            enemy.animator.CrossFade("Start_Move", 0);
        }

        public void Execute(EnemyStateMachine enemy)
        {        
            
            if (_retreatTime > 0)
            {
                _retreatTime -= Time.deltaTime;
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, _enemy.target.position,
                    -enemy.enemyData.moveSpeed * Time.deltaTime);
            }
            else
            {
                enemy.ChangeState(new BossAimState());
            }
        }

        public void Exit()
        {
            _retreatTime = 0;
        }
    }
}

