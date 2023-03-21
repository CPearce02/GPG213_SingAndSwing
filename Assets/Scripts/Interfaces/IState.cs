using UnityEngine;

namespace Interfaces
{
    public interface IState
    {
        void Enter(EnemyMovementManager enemy);
        void Execute(EnemyMovementManager enemy = null);
        void Exit();
        void OnTriggerEnter2D(Collider2D other);
    }
}