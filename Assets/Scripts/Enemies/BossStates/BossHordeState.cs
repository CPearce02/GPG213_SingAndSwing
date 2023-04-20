using Interfaces;
using UnityEngine;

namespace Enemies.BossStates
{
    public class BossHordeState : IState
    {
        private BossEnemyStateMachine _enemy;
        private SpawnEnemies _spawnEnemies;
        private Vector3 _closestPosition;
        private bool _inPosition;
        private float closestDistance = Mathf.Infinity;
        private bool _spawned;
        private float _spawningTime = 4;

        public void Enter(EnemyStateMachine enemy)
        {
            this._enemy = enemy as BossEnemyStateMachine;
            if (_enemy == null) return;
            _spawnEnemies = _enemy.GetComponentInChildren<SpawnEnemies>();
            _enemy.animator.CrossFade("Start_Move", 0);


        }

        public void Execute(EnemyStateMachine enemy)
        {
            //Get into Position
            FindAndMoveIntoPosition();
            if (!_inPosition) return;

            //Once in position - Start Spawning 
            if (_spawningTime > 0)
            {
                enemy.animator.CrossFade("Ability2_Idle", 0);
                //Start Spawning
                _spawningTime -= Time.deltaTime;
                if (_spawned) return;
                _spawnEnemies.StartSpawning(_enemy.target);
                _spawned = true;
            }
            //Then Dissappear 
            else
            {
                enemy.ChangeState(new BossDisappearState());
            }
        }

        public void Exit()
        {
            _spawned = false;
            _spawnEnemies.ClearEnemyList();
        }

        private void FindAndMoveIntoPosition()
        {
            //Find closest Postion
            foreach (Transform position in _enemy.positions)
            {
                float distance = Vector3.Distance(_enemy.transform.position, position.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _closestPosition = position.position;
                }
            }
            //Move to it

            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _closestPosition, _enemy.enemyData.moveSpeed * 2 * Time.deltaTime);

            _inPosition = true;
        }
    }
}
