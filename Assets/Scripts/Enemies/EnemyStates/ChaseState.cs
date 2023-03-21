using Interfaces;
using UnityEngine;

namespace Enemies.EnemyStates
{
    public class ChaseState : IState
    {
        private Transform playerTransform;
        
        public ChaseState(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
        }
        public void Enter(EnemyMovementManager enemy)
        {
            
        }

        public void Execute(EnemyMovementManager enemy)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, playerTransform.position, enemy.enemyData.moveSpeed * Time.deltaTime);
        }

        public void Exit()
        {
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
        }
    }
}