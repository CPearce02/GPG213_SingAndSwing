using Interfaces;
using UnityEngine;

namespace Enemies.EnemyStates
{
    public class AttackState : IState
    {
        
        bool _hasAttacked = false;
        private EnemyStateMachine _enemy;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy;
            Debug.Log("Attack state initiated");
        }

        public void Execute(EnemyStateMachine enemy = null)
        {
            
        }

        public void Exit()
        {
        }

        public void OnTriggerEnter2D(Collider2D other)
        { 
            Debug.Log("Attack state executed");
            if (!_hasAttacked)
            {
                Debug.Log("Enemy is attacking");
                _hasAttacked = true;
                _enemy.ChangeState(new RetreatState(other.transform, this._enemy));
            }
        }
    }
}