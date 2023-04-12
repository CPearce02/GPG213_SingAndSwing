using Interfaces;
using UnityEngine;
using Events;

namespace Enemies.BossStates
{
    public class BossProjectileState : IState
    {
        private BossEnemyStateMachine _enemy;

        private ShootingEnemy _shootingEnemy;

        private float _retreatTime = 4f;
        private Transform _aimDirection;
        private Quaternion _initialRotation;
        private Vector3 _centrePosition = new Vector3(0.5f, 3, 0);

        private float _shootingTime = 5f;
        private bool _inPosition;
        private bool _beenHit;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            _shootingEnemy = _enemy.GetComponentInChildren<ShootingEnemy>();
            _aimDirection = _shootingEnemy.transform;
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
            if(!_inPosition) return;
            
            //Check if the player has hit the boss
            if(!_beenHit)
            {
                //Start/Continue Shooting for 5 seconds
                if(_shootingTime > 0)
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
            else
            {
                //Interupted
                enemy.ChangeState(new BossInterruptedState());
            }
        }

        public void Exit()
        {
            //Stop Shooting 
            _shootingEnemy.SetDisableUpdate(true);
            //Reset Paramaters - Do I have to do this? 
            _beenHit = false;
            _inPosition = false;
        }

        private void MoveToCentre()
        {
            //FIND THE CENTRE TRANSFORM AND GO TO IT
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _centrePosition, _enemy.enemyData.moveSpeed * Time.deltaTime);
        }
 

        private void SpawnProjectiles()
        {
            _shootingEnemy.SetDisableUpdate(false);

            // Calculate the rotation angle based on the rotation sawpeed and time
            float rotationAngle = 200 * Time.deltaTime;

            // Rotate the object around its own z-axis by the rotation angle
            _aimDirection.Rotate(0f, 0f, rotationAngle * 2);
        }
    
    }
}
