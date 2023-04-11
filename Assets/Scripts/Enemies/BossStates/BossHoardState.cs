using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossHoardState: IState
    {
        private BossEnemyStateMachine _enemy;
        private SpawnEnemies _spawnEnemies;
        private Vector3 _centrePosition = new Vector3(0.5f, 3, 0);
        private bool _inPosition;
    
        private bool _spawned;
        private float _spawningTime = 3;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            _spawnEnemies = _enemy.GetComponentInChildren<SpawnEnemies>();
        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Get into Position
            MoveToCentre();
            if (!_inPosition) return;
            if (!_spawned)
            {
                //Start Spawning
                _spawnEnemies._canSpawn = true;
                _spawned = true;
            }
            else
            {
                //Disappear
            }

        }

        public void Exit()
        {
            _spawnEnemies._canSpawn = false;
            _spawned = false;
        }
        private void MoveToCentre()
        {
            if (_enemy.transform.position != _centrePosition && !_inPosition)
            {
                _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _centrePosition, _enemy.enemyData.moveSpeed * Time.deltaTime);
            }
            else
            {
                _inPosition = true;
            }
        }

    }
}
