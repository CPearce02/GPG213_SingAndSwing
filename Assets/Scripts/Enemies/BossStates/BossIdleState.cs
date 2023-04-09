using Interfaces;
using UnityEngine;
using Core.Player;

namespace Enemies.BossStates
{
    public class BossIdleState : IState
    {
        private BossEnemyStateMachine _enemy;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
        }

        public void Execute(EnemyStateMachine enemy)
        {
            _enemy.animator.CrossFade("Idle", 0);

            //If player is within range 
            var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.triggerRange, enemy.PlayerLayer);
            if (hit != null && hit.TryGetComponent(out PlatformingController player))
            {
                var transform = player.transform;
                _enemy.target = transform;
                _enemy.ChangeState(new BossAimState(transform));
            }
        }

        public void Exit()
        {

        }
    }
}

