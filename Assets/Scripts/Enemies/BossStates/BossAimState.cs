using UnityEngine;
using Interfaces;
using System.Linq.Expressions;
using UnityEngine.Rendering;

namespace Enemies.BossStates
{
    public class BossAimState : IState
    {
        private Transform _playerTransform;
        Vector2 _directionOfTravel;
        private EnemyStateMachine _enemy;
        private int _attackType = 1;

        public BossAimState(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy;
            //_attackType = Random.Range(1, 4);
        }

        public void Execute(EnemyStateMachine enemy)
        {
            if (_playerTransform == null)
            {
                Debug.Log("Player was not found");
            }
            
            //Aim towards player 
            _directionOfTravel = _playerTransform.position - enemy.transform.position;
            _directionOfTravel = _directionOfTravel.normalized;

            //CHOOSE ATTACK
            switch (_attackType)
            {
                case 1:
                    Charge();
                    break;
                case 2:
                    //Projectile
                    break;
                case 3:
                    //Hoard
                    break;
            }
        }

        public void Exit()
        {
        }

        private void Charge()
        {
            //Wait


            //Charge
            _enemy.ChangeState(new BossChargeState(_directionOfTravel, _playerTransform));
        }
        
    }
}

