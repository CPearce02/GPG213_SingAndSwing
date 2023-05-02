using Interfaces;
using UnityEngine;
using Events;

namespace Enemies.BossStates
{
    public class BossProjectileState : IState
    {
        private BossEnemyStateMachine _enemy;

        private ShootingEnemy _shootingEnemy;

        private Transform _aimDirection;
        private Vector3 _centrePosition;

        private float _shootingTime = 5f;
        private bool _inPosition;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            if (_enemy == null) return;
            _shootingEnemy = _enemy.GetComponentInChildren<ShootingEnemy>();
            _aimDirection = _shootingEnemy.transform;
            _centrePosition = _enemy.positions[0].position;
            _enemy.AudioSource.PlayOneShot(_enemy.BossProjectileAttack);
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Get into Position
            if (enemy.transform.position != _centrePosition && !_inPosition)
            {
                MoveToCentre();
            }
            else
            {
                _inPosition = true;
            }
            if (!_inPosition) return;
            enemy.animator.CrossFade("AOE_Attack", 0);
            //Start/Continue Shooting for 5 seconds
            if (_shootingTime > 0)
            {
                _shootingTime -= Time.deltaTime;
                SpawnProjectiles();
            }
            else
            {
                //Start Recharging 
                enemy.ChangeState(new BossRechargeShieldState());
            }

        }

        public void Exit()
        {
            //Stop Shooting 
            _shootingEnemy.SetDisableUpdate(true);
            _inPosition = false;
        }

        private void MoveToCentre()
        {
            _enemy.animator.CrossFade("Start_Move", 0);
            //FIND THE CENTRE TRANSFORM AND GO TO IT
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _centrePosition, _enemy.enemyData.moveSpeed * 2 * Time.deltaTime);
        }


        private void SpawnProjectiles()
        {
            _shootingEnemy.SetDisableUpdate(false);

            // Calculate the rotation angle based on the rotation speed and time
            float rotationAngle = 200 * Time.deltaTime;

            // Rotate the object around its own z-axis by the rotation angle
            _aimDirection.Rotate(0f, 0f, rotationAngle * 2);
        }

    }
}
