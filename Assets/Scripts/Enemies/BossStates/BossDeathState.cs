using Events;
using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossDeathState : IState
    {
        private BossEnemyStateMachine _enemy;
        private SpriteRenderer _bossSpriteRenderer;
        private Collider2D _bossCollider;


        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            if (_enemy == null) return;

            _bossSpriteRenderer = _enemy.SpriteRenderer;
            _bossCollider = _enemy.MainCollider;
            
            _enemy.AudioSource.PlayOneShot(_enemy.BossDeath);

            GameEvents.onBossFightEndEvent?.Invoke();
        }

        public void Execute(EnemyStateMachine enemy)
        {
            enemy.animator.CrossFade("Death", 0);
            // Debug.Log("Boss Died!");
        }

        public void Exit()
        {

        }
    }
}