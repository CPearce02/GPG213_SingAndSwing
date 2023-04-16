using Core.Player;
using Enemies;
using Interfaces;
using System.Linq.Expressions;
// using Mono.Cecil.Cil;
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
        private Collider2D[] collisions;
        private static LayerMask Mask => LayerMask.GetMask("Player") | LayerMask.GetMask("Environment");

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
            if (!doCharge && _enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Ability1_Start"))
            {
                if (_enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    doCharge = true;
                }
            }

            if (!hasHit && doCharge)
            {
                //Charge towards player direction - Charge until it hits something 
                _enemy.Rb.AddForce(_directionToCharge * (_enemy.enemyData.moveSpeed * 5 * _enemy.enemyData.chargeSpeedMultiplier * Time.fixedDeltaTime));
                _enemy.animator.CrossFade(Ability1Idle, 0);

                //Check to see what the boss hit 
                var hit = Physics2D.OverlapCircle(_enemy.ChargeTransform.position, _enemy.enemyData.chargeAttackSize, Mask);
                if (hit != null && hit.TryGetComponent(out HealthManager player))
                {
                    if (hasHit)
                        return;

                    Debug.Log("Hitplayer");
                    _enemy.Rb.velocity = Vector2.zero;
                    hasHit = true;
                    player.TakeDamage(_enemy.enemyData.chargeDamage);
                    var playerRb = player.GetComponent<Rigidbody2D>();
                    playerRb.velocity = Vector2.zero;
                }
                //hit the environment
                else if (hit != null && hit.TryGetComponent(out TilemapCollider2D environment))
                {
                    Debug.Log("Hit wall");

                    //stunned
                    if (!_enemy.CanBeStunned) return;

                    _enemy.CanBeStunned = false;
                    _enemy.Rb.velocity = Vector2.zero;

                    _enemy.ChangeState(new BossStunState());
                }

            }

            FromIdleToEnd(_enemy);

            FromEndToExit(_enemy);
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

