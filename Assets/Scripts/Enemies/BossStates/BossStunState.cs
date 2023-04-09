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
        private Transform _playerTransform;
        float _stunTime;
        
        static readonly int Stunned = Animator.StringToHash("Stunned");
        
        public BossStunState(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            _stunTime = enemy.enemyData.retreatTime;
            CameraShakeEvent cameraShakeEvent = new CameraShakeEvent(Strength.High, 0.5f, true);
            cameraShakeEvent.Invoke();
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Stun animation
            // Wait with timer
            if(_stunTime > 0)
            {
                _stunTime -= Time.deltaTime;
                enemy.animator.CrossFade(Stunned, 0);
            }
            else
            {
                _enemy.ChangeState(new BossAimState(_playerTransform));
            }

            //Idle state
            //enemy.ChangeState(new BossIdleState());
        }

        public void Exit()
        {
        }
    }
}

