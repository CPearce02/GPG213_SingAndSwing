using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using UnityEngine.Serialization;


public class SpawnEnemies : MonoBehaviour
{
   [SerializeField] private GameObject _enemyPrefab;

    private List<GameObject> spawnedEnemies = new();

    private int spawnLimit = 3;
    [FormerlySerializedAs("_canSpawn")] public bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!canSpawn)
        {

        }
        else
        {
            StartCoroutine(SpawnEnemy());
        }
    }


    public void SetSpawnAmount(int value) => spawnLimit = value;

    IEnumerator SpawnEnemy()
    {
        if (spawnedEnemies.Count <=  spawnLimit)
        {
            var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
            enemy.GetComponent<EnemyStateMachine>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 300);
            yield return new WaitForSeconds(0.25f);
            enemy.GetComponent<EnemyStateMachine>().enabled = true;
        }
        canSpawn = false;
    }
}
