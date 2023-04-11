using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossProjectileState : IState
    {
        private Transform _playerTransform;
        private BossEnemyStateMachine _enemy;

        private ShootingEnemy _shootingEnemy;

        private float _retreatTime = 4f;
        private Transform _aimDirection;
        private Quaternion _initialRotation;
        private Vector3 _centrePosition = new Vector3(0.5f, 3, 0);

        private float _shootingTime = 5f;
        private bool _inPosition;

        public BossProjectileState(Transform playerTransform){
            this._playerTransform = playerTransform;
        }

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            // _shootingEnemy = _enemy.GetComponentInChildren<ShootingEnemy>();
            _shootingEnemy = _enemy.GetComponent<ShootingEnemy>();
            _aimDirection = _shootingEnemy.transform;
            _initialRotation = _aimDirection.rotation;

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

            //Start Shooting for 5 seconds
            if(_shootingTime > 0)
            {
                _shootingTime -= Time.deltaTime;
                SpawnProjectiles();
                //MoveAway();
            }
            else
            {
                //Stop shooting
                enemy.ChangeState(new BossRetreatState(_playerTransform));
            }
        }

        public void Exit()
        {
            _shootingEnemy.SetDisableUpdate(true);
            _aimDirection.rotation = _initialRotation;
            //If not has taken damage then set shield back on 
            _enemy.GetComponent<Enemy>().SetCanBeDestroyed(false);
        }

        private void MoveToCentre()
        {
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _centrePosition, _enemy.enemyData.moveSpeed * Time.deltaTime);
        }

        //I cant decide whether I want the enemy to be stationary during this attack or not / if so then move away or towards ?  
        private void MoveAway()
        {
            if (_retreatTime > 0)
            {
                _retreatTime -= Time.deltaTime;
                _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _playerTransform.position,
                    _enemy.enemyData.moveSpeed * Time.deltaTime);
            }
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
