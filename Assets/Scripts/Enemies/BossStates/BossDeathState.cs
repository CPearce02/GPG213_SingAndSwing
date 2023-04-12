using Events;
using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossDeathState : IState
    {
        public void Enter(EnemyStateMachine enemy)
        {
            GameEvents.onBossFightEndEvent?.Invoke();
        }

        public void Execute(EnemyStateMachine enemy = null)
        {
            // Debug.Log("Boss Died!");
        }

        public void Exit()
        {
            
        }
    }
}