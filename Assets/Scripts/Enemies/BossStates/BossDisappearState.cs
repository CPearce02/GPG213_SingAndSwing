using Enemies;
using Enemies.BossStates;
using Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDisappearState : IState
{
    private BossEnemyStateMachine _enemy;

    private SpriteRenderer _bossSpriteRenderer;
    private float _disappearTime = 6f;

    public void Enter(EnemyStateMachine enemy)
    {
        this._enemy = enemy as BossEnemyStateMachine;
        if (_enemy == null) return;
        _bossSpriteRenderer = _enemy.GetComponentInChildren<SpriteRenderer>();
        _bossSpriteRenderer.enabled = false;
    }

    public void Execute(EnemyStateMachine enemy )
    {
        if(_disappearTime > 0)
        {
            _disappearTime -= Time.deltaTime;

            Vector2 _directionToCharge = _enemy.transform.position - _enemy.target.position;
            // Check if there is a wall in front of the enemy
            var hit = Physics2D.OverlapCircle(enemy.transform.position, enemy.enemyData.attackRange - 0.5f);
            if(hit.TryGetComponent(out TilemapCollider2D environment))
            {
                //Change Direction
                Debug.Log("Hit Wall");
                _directionToCharge = -_directionToCharge;
            }
            //Retreat away
            enemy.Rb.AddForce(_directionToCharge * (enemy.enemyData.moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            enemy.ChangeState(new BossRechargeShieldState());
        }
    }

    public void Exit()
    {
        _bossSpriteRenderer.enabled = true;
    }
}
