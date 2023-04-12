using Core.Player;
using Enemies;
using Interfaces;
using System.Linq.Expressions;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemies.BossStates
{
    public class BossChargeState : IState
    {
        private Vector2 _directionToCharge;

        private BossEnemyStateMachine _enemy;
        private bool hasHit;
        private bool doCharge;
        
        static readonly int Ability1Start = Animator.StringToHash("Ability1_Start");
        static readonly int Ability1Idle = Animator.StringToHash("Ability1_Idle");
        static readonly int Ability1End = Animator.StringToHash("Ability1_End");

        public void Enter(EnemyStateMachine enemy)
        { 
            enemy.animator.CrossFade(Ability1Start, 0);
            this._enemy = enemy as BossEnemyStateMachine;
            
            //Aim towards player 
            _directionToCharge = _enemy.target.position - enemy.transform.position;
            _directionToCharge = _directionToCharge.normalized;
        }

        public void Execute(EnemyStateMachine enemy)
        {
            if(!doCharge && enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Ability1_Start"))
            {
                if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    doCharge = true;
                }
            }
            
            if(!hasHit && doCharge) 
            {
                //Charge towards player direction - Charge until it hits something 
                enemy.Rb.AddForce(_directionToCharge * (enemy.enemyData.moveSpeed * 100 * Time.fixedDeltaTime));
                enemy.animator.CrossFade(Ability1Idle, 0);

                //Check to see what the boss hit 
                var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.attackRange - 1);
                if (hit.TryGetComponent(out HealthManager player))
                {
                    if (hasHit)
                        return;

                    Debug.Log("Hitplayer");
                    enemy.Rb.velocity = Vector2.zero;
                    hasHit = true;
                }
                //hit the environment
                else if (hit.TryGetComponent(out TilemapCollider2D environment))
                {
                    Debug.Log("Hit wall");
                    
                    //stop
                    enemy.Rb.velocity = Vector2.zero;
                    //stunned
                    var boss = enemy as BossEnemyStateMachine;
                    if (boss == null) return;
                    if(boss.canBeStunned)
                    {
                        boss.canBeStunned = false;
                        enemy.Rb.velocity = Vector2.zero;

                        enemy.ChangeState(new BossStunState());
                    }
                }
                
            }

            FromIdleToEnd(enemy);

            FromEndToExit(enemy);
        }

        private void FromEndToExit(EnemyStateMachine enemy)
        {
            if (hasHit && enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Ability1_End"))
            {
                if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    enemy.Rb.velocity = Vector2.zero;
                    enemy.ChangeState(new BossRetreatState());
                }
            }
        }

        private void FromIdleToEnd(EnemyStateMachine enemy)
        {
            if (hasHit && enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Ability1_Idle"))
            {
                enemy.Rb.velocity = Vector2.zero;
                enemy.animator.CrossFade(Ability1End, 0);
            }
        }

        public void Exit()
        {
            hasHit = false;
            doCharge = false;
        }
    }
}

