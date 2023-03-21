using Core.Player;
using Interfaces;
using UnityEngine;

namespace Enemies.EnemyStates
{
    public class IdleState : IState
    {
        private EnemyMovementManager _enemy;
        public void Enter(EnemyMovementManager enemy)
        {
            this._enemy = enemy;
        }

        public void Execute(EnemyMovementManager enemy)
        {
            
        }

        public void Exit()
        {
            
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent(out PlatformingController player);
            
            if(!player) return;

            _enemy.CurrentState = new ChaseState(player.transform);
            
        }
    }
}