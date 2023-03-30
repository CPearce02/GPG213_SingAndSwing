using Core.Player;
using Enemies;
using Interfaces;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemies.BossStates
{
    public class BossChargeState : MonoBehaviour, IState
    {
        private Vector2 _directionToCharge;

        public BossChargeState(Vector2 direction)
        {
            this._directionToCharge = direction;
        }
        public void Enter(EnemyStateMachine enemy)
        {
           
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Charge towards player direction - Charge until it hits something 
            enemy.Rb.AddForce(_directionToCharge * (enemy.enemyData.moveSpeed * 100 * Time.fixedDeltaTime));

            //Check to see what the boss hit 
            var hit = Physics2D.OverlapCircle(enemy.transform.position, 1f);
            if (hit != null && hit.TryGetComponent(out PlatformingController player))
            {
                Debug.Log("Hitplayer");
                
                //Deal damage to player 

                //Retreat away 
                enemy.ChangeState(new BossRetreatState(player.transform));
            }
            //hit the environment
            else if (hit!= null && hit.TryGetComponent(out TilemapCollider2D environment))
            {
                Debug.Log("Hit wall");

                //stop
                enemy.Rb.velocity = Vector2.zero;
                //stunned
                enemy.ChangeState(new BossStunState());
            }
        }

        public void Exit()
        {

        }
    }
}

