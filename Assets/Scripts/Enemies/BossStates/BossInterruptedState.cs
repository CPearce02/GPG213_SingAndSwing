using Enemies;
using Enemies.BossStates;
using Interfaces;
using UnityEngine;

public class BossInterruptedState : IState
{
    public void Enter(EnemyStateMachine enemy)
    {
    }

    public void Execute(EnemyStateMachine enemy)
    {
        //Animation


        //Finish Animation & Return to Aim State
        enemy.Rb.velocity = Vector2.zero;
        Debug.Log("INTERRUPTED");
        // enemy.ChangeState(new BossAimState());
    }

    public void Exit()
    {
    }
}
