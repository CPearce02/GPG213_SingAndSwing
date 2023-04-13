using Interfaces;
using UnityEngine;
using Core.Player;
using Events;

namespace Enemies.BossStates
{
    public class BossIdleState : IState
    {
        private BossEnemyStateMachine _enemy;
        bool hasWokenUp;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
        }

        public void Execute(EnemyStateMachine enemy)
        {

            //If player is within range 
            var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.triggerRange, enemy.PlayerLayer);
            if (hit == null && !hasWokenUp) _enemy.animator.CrossFade("Idle", 0);
            if (hit != null && hit.TryGetComponent(out PlatformingController player) && !hasWokenUp)
            {
                hasWokenUp = true;
                var transform = player.transform;
                _enemy.target = transform;
                SendBossStarted();
            }

            if (!hasWokenUp) return;

            Wake();
        }

        public void Exit()
        {

        }

        public void SendBossStarted()
        {
            if (_enemy.HasBeenActivated) return;

            _enemy.SetHasBeenActivated();
            GameEvents.onBossFightStartEvent?.Invoke(_enemy.enemyData);
            // Debug.Log("Boss Fight Started");
        }

        void Wake()
        {
            _enemy.animator.SetTrigger("Wake");
            Debug.Log("Wake");
            if (_enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_3"))
            {
                Debug.Log("Idle 3");
                if (_enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                    _enemy.ChangeState(new BossAimState());
            }
        }
    }
}

