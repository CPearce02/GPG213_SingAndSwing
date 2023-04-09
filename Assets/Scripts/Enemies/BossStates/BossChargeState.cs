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
        Transform _playerTransform;
        private bool hasHit;
        private bool doCharge;
        
        
        static readonly int Ability1Start = Animator.StringToHash("Ability1_Start");
        static readonly int Ability1Idle = Animator.StringToHash("Ability1_Idle");
        static readonly int Ability1End = Animator.StringToHash("Ability1_End");


        public BossChargeState(Vector2 direction, Transform playerTransform)
        {
            this._directionToCharge = direction;
            this._playerTransform = playerTransform;
        }
        public void Enter(EnemyStateMachine enemy)
        { 
            enemy.animator.CrossFade(Ability1Start, 0);
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
                var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.attackRange);
                if (hit.TryGetComponent(out HealthManager player))
                {
                    if (hasHit)
                        return;

                    Debug.Log("Hitplayer");
                    _playerTransform = player.transform;
                    enemy.Rb.velocity = Vector2.zero;
                    hasHit = true;
                    //Deal damage to player 

                    //Retreat away 
                }
                //hit the environment
                else if (hit.TryGetComponent(out TilemapCollider2D environment))
                {
                    Debug.Log("Hit wall");

                    //stop
                    enemy.Rb.velocity = Vector2.zero;
                    //stunned
                    enemy.ChangeState(new BossStunState(_playerTransform));
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
                    enemy.ChangeState(new BossRetreatState(_playerTransform));
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

        }
    }
}

