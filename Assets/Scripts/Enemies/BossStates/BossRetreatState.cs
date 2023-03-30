using Enemies.EnemyStates;
using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossRetreatState : IState
    {
        private Transform _playerTransform;
        private EnemyStateMachine _enemy;
        float _retreatTime;
        public BossRetreatState(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }
        public void Enter(EnemyStateMachine enemy)
        {
            _retreatTime = enemy.enemyData.retreatTime;
        }

        public void Execute(EnemyStateMachine enemy)
        {
            _retreatTime -= Time.deltaTime;
            if (_retreatTime > 0)
            {
                //enemy.animator.CrossFade("Move", 0);
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, _playerTransform.position,
                    -enemy.enemyData.moveSpeed * Time.deltaTime);
            }
            else
            {
                //_enemy.ChangeState(new BossAimState(_playerTransform));
            }
        }

        public void Exit()
        {
            _retreatTime = 0;
        }
    }
}

