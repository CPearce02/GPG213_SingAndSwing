using Interfaces;
using UnityEngine;
using Effects;

namespace Enemies.BossStates
{
    public class BossRechargeShieldState : IState
    {
        private BossEnemyStateMachine _enemy;
        private Enemy _enemyComponent;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            _enemyComponent = _enemy.GetComponent<Enemy>();
            if (_enemy == null) return;
            _enemy.animator.CrossFade("Recharge", 0);
            _enemy.AudioSource.PlayOneShot(_enemy.BossRechargeShield);
            //Choose new combo 
            _enemy.enemyData.Combo = _enemy.comboList[Random.Range(0,_enemy.comboList.Length)];
            _enemy.GetComponentInChildren<ShieldHandler>().ChangeColour(0);
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Animation

            //When finish aninamtion - Turn on sheild 
            _enemyComponent.SetCanBeDestroyed(false);
            enemy.ChangeState(new BossAimState());
        }

        public void Exit()
        {

        }
    }
}