using Enemies;
using Enemies.BossStates;
using Interfaces;
using UnityEngine;

public class BossInterruptedState : IState
{
    private float _interupptedWaitTime = 3;
    public void Enter(EnemyStateMachine enemy)
    {
    }

    public void Execute(EnemyStateMachine enemy)
    {
        Debug.Log("Interrupted");
        //Animation
        

        if(_interupptedWaitTime > 0 )
        {
            _interupptedWaitTime -= Time.deltaTime;
            //Stop any movement
            enemy.Rb.velocity = Vector2.zero;
        }
        else
        {
            //Finish Animation & Return to Aim State
            enemy.ChangeState(new BossAimState());
        }
        
    }

    public void Exit()
    {
    }
}
