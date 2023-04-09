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
        private BossEnemyStateMachine _enemy;
        float decideAttackTime;
        private int _attackType = 1;

        public BossAimState(Transform playerTransform)
        {
            this._playerTransform = playerTransform;
        }

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            if (_enemy != null) decideAttackTime = _enemy.decideAttackTime;
            //_attackType = Random.Range(1, 4);
        }

        public void Execute(EnemyStateMachine enemy)
        {
            if(decideAttackTime > 0)
            {
                decideAttackTime -= Time.deltaTime;
                return;
            }
            
            decideAttackTime = _enemy.decideAttackTime;

            //Aim towards player 
            _directionOfTravel = _playerTransform.position - enemy.transform.position;
            _directionOfTravel = _directionOfTravel.normalized;

            DecideAttack();
        }

        private void DecideAttack()
        {
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

