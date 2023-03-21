using Core.Player;
using Interfaces;
using UnityEngine;

namespace Enemies.EnemyStates
{
    public class IdleState : IState
    {
        private EnemyStateMachine _enemy;
        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy;
        }

        public void Execute(EnemyStateMachine enemy)
        {
            
        }

        public void Exit()
        {
            
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent(out PlatformingController player);
            
            if(!player) return;

            _enemy.ChangeState(new ChaseState(player.transform));
        }
    }
}