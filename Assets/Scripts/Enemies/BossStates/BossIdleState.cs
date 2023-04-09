using Interfaces;
using UnityEngine;
using Core.Player;

namespace Enemies.BossStates
{
    public class BossIdleState : IState
    {
        private EnemyStateMachine _enemy;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy;
        }

        public void Execute(EnemyStateMachine enemy)
        {
            _enemy.animator.CrossFade("Idle", 0);

            //If player is within range 
            var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.triggerRange, enemy.PlayerLayer);
            if (hit != null && hit.TryGetComponent(out PlatformingController player))
            {
                _enemy.ChangeState(new BossAimState(player.transform));
            }
        }

        public void Exit()
        {

        }
    }
}

