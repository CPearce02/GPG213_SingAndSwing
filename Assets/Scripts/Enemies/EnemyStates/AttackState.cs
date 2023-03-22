using Core.Player;
using Interfaces;
using UnityEngine;


namespace Enemies.EnemyStates
{
    public class AttackState : IState
    {
        
        bool _hasAttacked = false;
        private EnemyStateMachine _enemy;
        private Transform playerTransform;
        public static readonly int Attack = Animator.StringToHash("Attack");

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy;
            Debug.Log("Attack state initiated");
        }

        public void Execute(EnemyStateMachine enemy)
        {
            if(!_hasAttacked)
            {
                var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.attackRange, enemy.PlayerLayer);
                if (hit != null && hit.TryGetComponent(out PlatformingController player))
                {
                    Debug.Log("Enemy is attacking");
                    enemy.animator.SetTrigger(Attack);
                    _hasAttacked = true;
                    playerTransform = player.transform;
                    hit = null;
                }    
            }
            
            if (_hasAttacked && enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                Debug.Log("has attacked and is in attack anim");
                if( enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    _enemy.ChangeState(new RetreatState(playerTransform, this._enemy));
                }
            }
            
        }

        public void Exit()
        {
            _hasAttacked = false;
        }
        
    }
}