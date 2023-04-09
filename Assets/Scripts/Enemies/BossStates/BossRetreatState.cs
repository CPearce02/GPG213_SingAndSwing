using Enemies.EnemyStates;
using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossRetreatState : IState
    {
        private Transform _playerTransform;
        private BossEnemyStateMachine _enemy;
        float _retreatTime;
        public BossRetreatState(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }
        public void Enter(EnemyStateMachine enemy)
        {
            _retreatTime = enemy.enemyData.retreatTime;
            enemy.animator.CrossFade("Start_Move", 0);
        }

        public void Execute(EnemyStateMachine enemy)
        {
            // var distance = Vector2.Distance(enemy.transform.position, _playerTransform.position);
            // if (distance > enemy.enemyData.triggerRange)
            // {
            //     _enemy.ChangeState(new BossAimState(_playerTransform));
            // }
            
            
            
            if (_retreatTime > 0)
            {
                _retreatTime -= Time.deltaTime;
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, _playerTransform.position,
                    -enemy.enemyData.moveSpeed * Time.deltaTime);
            }
            else
            {
                enemy.ChangeState(new BossAimState(_playerTransform));
            }
        }

        public void Exit()
        {
            _retreatTime = 0;
        }
    }
}

