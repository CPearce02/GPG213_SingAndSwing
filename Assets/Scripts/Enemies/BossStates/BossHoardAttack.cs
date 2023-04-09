using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enemies.BossStates;
using Enemies;

public class BossHoardAttack : IState
{
    private BossEnemyStateMachine _enemy;
    private Vector3 _centrePosition = new Vector3(0.5f, 3, 0);
    private bool _inPosition;

    public void Enter(EnemyStateMachine enemy)
    {
        this._enemy = enemy as BossEnemyStateMachine;

    }

    public void Execute(EnemyStateMachine enemy)
    {
        //Get into Position
        if (enemy.transform.position != _centrePosition && !_inPosition)
        {
            MoveToCentre();
        }
        else
        {
            _inPosition = true;
        }
        if (!_inPosition) return;

        
    }

    public void Exit()
    {

    }
    private void MoveToCentre()
    {
        _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _centrePosition, _enemy.enemyData.moveSpeed * Time.deltaTime);
    }
}
