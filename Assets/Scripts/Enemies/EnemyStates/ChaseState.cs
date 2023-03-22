using System;
using Interfaces;
using UnityEngine;

namespace Enemies.EnemyStates
{
    public class ChaseState : IState
    {
        private Transform _playerTransform;
        Vector2 _directionOfTravel;

        public ChaseState(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }
        
        public void Enter(EnemyStateMachine enemy)
        {
            
        }

        public void Execute(EnemyStateMachine enemy)
        {
            enemy.animator.CrossFade("Move", 0);
            _directionOfTravel = _playerTransform.position - enemy.transform.position;

            _directionOfTravel = _directionOfTravel.normalized;

            enemy.Rb.AddForce(_directionOfTravel * (enemy.enemyData.moveSpeed * 10 * Time.fixedDeltaTime));
            if(Vector2.Distance(enemy.transform.position, _playerTransform.position) < enemy.enemyData.attackRange)
            {
                enemy.Rb.velocity = Vector2.zero;
                enemy.ChangeState(new AttackState());
            }
        }

        public void Exit()
        {
        }
        
    }
}