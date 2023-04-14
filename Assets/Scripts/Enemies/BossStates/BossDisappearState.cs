using Enemies;
using Enemies.BossStates;
using Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossDisappearState : IState
{
    private BossEnemyStateMachine _enemy;

    private SpriteRenderer _bossSpriteRenderer;
    private Collider2D _bossCollider;
    private float _disappearTime;

    public void Enter(EnemyStateMachine enemy)
    {
        this._enemy = enemy as BossEnemyStateMachine;
        if (_enemy == null) return;

        _disappearTime = _enemy.enemyData.disappearTime;
        _bossSpriteRenderer = _enemy.SpriteRenderer;
        _bossCollider = _enemy.MainCollider;

        Disappear(enemy);

        Debug.Log("Disappear");
    }


    public void Execute(EnemyStateMachine enemy)
    {
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Disappear"))
        {
            if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                _bossSpriteRenderer.enabled = false;
            }
        }

        if (_disappearTime > 0)
        {
            _disappearTime -= Time.deltaTime;
            _bossCollider.enabled = false;
            enemy.Rb.velocity = Vector2.zero;
        }

        if (_disappearTime <= 0.001)
        {
            _bossSpriteRenderer.enabled = true;
            _bossCollider.enabled = true;

            Appear(enemy);
        }

        if (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("Appear"))
        {
            if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemy.ChangeState(new BossRechargeShieldState());
            }
        }
    }

    public void Exit()
    {
        ResetAnimationStates();
    }

    private static void Appear(EnemyStateMachine enemy)
    {
        if (enemy.animator.GetBool("Appear"))
            return;

        enemy.animator.SetBool("Appear", true);
        enemy.animator.SetBool("Disappear", false);
    }

    private static void Disappear(EnemyStateMachine enemy)
    {
        if (enemy.animator.GetBool("Disappear"))
            return;

        enemy.animator.SetBool("Appear", false);
        enemy.animator.SetBool("Disappear", true);
    }

    private void ResetAnimationStates()
    {
        _enemy.animator.SetBool("Appear", false);
        _enemy.animator.SetBool("Disappear", false);
    }


}
