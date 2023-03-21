using UnityEngine;

namespace Interfaces
{
    public interface IState
    {
        void Enter(EnemyStateMachine enemy);
        void Execute(EnemyStateMachine enemy = null);
        void Exit();
        void OnTriggerEnter2D(Collider2D other);
    }
}