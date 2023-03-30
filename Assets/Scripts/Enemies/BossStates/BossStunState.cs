using Interfaces;
using Enemies;
using Enemies.BossStates;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossStunState : IState
    {
        public void Enter(EnemyStateMachine enemy)
        {
        }

        public void Execute(EnemyStateMachine enemy = null)
        {
            //Stun animation
            Debug.Log("STUNNED");
            //Wait

            //Idle state
            //enemy.ChangeState(new BossIdleState());
        }

        public void Exit()
        {
        }
    }
}

