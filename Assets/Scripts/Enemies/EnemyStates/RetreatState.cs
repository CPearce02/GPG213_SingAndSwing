using System;
using Interfaces;
using UnityEngine;

namespace Enemies.EnemyStates
{
    public class RetreatState : IState
    {
        private Transform _playerTransform;
        private EnemyStateMachine _enemy;
        float _retreatTime;
        
        public RetreatState(Transform playerTransform, EnemyStateMachine enemy)
        {
            this._playerTransform = playerTransform;
            this._enemy = enemy;
        }
        
        public void Enter(EnemyStateMachine enemy)
        {
            _retreatTime = enemy.enemyData.retreatTime;
        }

        public void Execute(EnemyStateMachine enemy)
        {
            _retreatTime -= Time.deltaTime;
            if(_retreatTime > 0)
            {
                enemy.animator.CrossFade("Move", 0);
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, _playerTransform.position,
                    -enemy.enemyData.moveSpeed * Time.deltaTime);
            }
            else
            {
                _enemy.ChangeState(new ChaseState(_playerTransform));
            }
            
        }

        public void Exit()
        {
            _retreatTime = 0;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
        }
        
    }
}