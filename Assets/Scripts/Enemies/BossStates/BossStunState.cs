using Interfaces;
using Enemies;
using Enemies.BossStates;
using Enums;
using Structs;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossStunState : IState
    {
        private BossEnemyStateMachine _enemy;
        float _stunTime;

        static readonly int Stunned = Animator.StringToHash("Stunned");

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            if (_enemy == null) return;
            _stunTime = enemy.enemyData.stunTime;
            _enemy.AudioSource.PlayOneShot(_enemy.BossStunned);
            CameraShakeEvent cameraShakeEvent = new CameraShakeEvent(Strength.High, 0.5f, true);
            cameraShakeEvent.Invoke();
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Stun animation
            // Wait with timer
            if (_stunTime > 0)
            {
                _stunTime -= Time.deltaTime;
                enemy.animator.CrossFade(Stunned, 0);
            }
            else
            {
                _enemy.ChangeState(new BossAimState());
            }
        }

        public void Exit()
        {
        }
    }
}

