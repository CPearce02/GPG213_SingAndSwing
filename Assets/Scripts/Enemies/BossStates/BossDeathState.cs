using Events;
using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossDeathState : IState
    {
        public void Enter(EnemyStateMachine enemy)
        {
        }

        public void Execute(EnemyStateMachine enemy)
        {
            enemy.animator.CrossFade("Death", 0);
            // Debug.Log("Boss Died!");
        }

        public void Exit()
        {
            GameEvents.onBossFightEndEvent?.Invoke();
        }
    }
}