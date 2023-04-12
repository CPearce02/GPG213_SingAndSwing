using UnityEngine;
using Interfaces;
using System.Linq.Expressions;
using UnityEngine.Rendering;

namespace Enemies.BossStates
{
    public class BossAimState : IState
    {
        private BossEnemyStateMachine _enemy;
        float decideAttackTime;
        private int _attackType = 3;


        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            if (_enemy != null) decideAttackTime = _enemy.decideAttackTime;

            //If it has no shield then it chooses between Projectile or Hoard
            if(_enemy.GetComponent<Enemy>().CanBeDestroyed)
            {
                _attackType = Random.Range(2,4);
            }
            //Else charge
            else
            {
                _attackType = 1;
            }
        }

        public void Execute(EnemyStateMachine enemy)
        {
            if(decideAttackTime > 0)
            {
                decideAttackTime -= Time.deltaTime;
                return;
            }
            
            decideAttackTime = _enemy.decideAttackTime;

            DecideAttack();
        }

        private void DecideAttack()
        {
            //CHOOSE ATTACK
            switch (_attackType)
            {
                case 1:
                    _enemy.ChangeState(new BossChargeState());
                    break;
                case 2:
                    //Projectile
                    _enemy.ChangeState(new BossProjectileState());
                    break;
                case 3:
                    //Hoard
                    _enemy.ChangeState(new BossHoardState());
                    break;
            }
        }

        public void Exit()
        {
        }

    }
}

