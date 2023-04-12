using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using UnityEngine.Serialization;
using Enemies.EnemyStates;

public class SpawnEnemies : MonoBehaviour
{
   [SerializeField] private GameObject _enemyPrefab;

    private List<GameObject> spawnedEnemies = new();

    private int spawnLimit = 3;

    public void SetSpawnAmount(int value) => spawnLimit = value;

    public void StartSpawning(Transform target) => StartCoroutine(SpawnEnemy(target));
    
    IEnumerator SpawnEnemy(Transform target)
    {
        while(spawnedEnemies.Count < spawnLimit)
        {
            yield return new WaitForSeconds(1f);
            var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
            enemy.GetComponent<EnemyStateMachine>().ChangeState(new ChaseState(target));

            if(spawnedEnemies.Count >= spawnLimit)
            {   
                yield break;
            }
        }
    }
}
