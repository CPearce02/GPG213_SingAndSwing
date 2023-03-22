using System;
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
            enemy.animator.CrossFade("Idle", 0);

            var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.triggerRange, enemy.PlayerLayer);
            if (hit != null && hit.TryGetComponent(out PlatformingController player))
            {
                _enemy.ChangeState(new ChaseState(player.transform));
            }        
        }

        public void Exit()
        {
            
        }
    }
}