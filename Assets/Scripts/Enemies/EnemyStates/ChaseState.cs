using Interfaces;
using UnityEngine;

namespace Enemies.EnemyStates
{
    public class ChaseState : IState
    {
        private Transform _playerTransform;
        
        public ChaseState(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }
        
        public void Enter(EnemyStateMachine enemy)
        {
            
        }

        public void Execute(EnemyStateMachine enemy)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, _playerTransform.position, enemy.enemyData.moveSpeed * Time.deltaTime);
            if(Vector2.Distance(enemy.transform.position, _playerTransform.position) < enemy.enemyData.attackRange)
            {
                enemy.ChangeState(new AttackState());
            }
        }

        public void Exit()
        {
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
        }
    }
}