using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemyManager : MonoBehaviour
{
    //Scriptable object
    public List<Enemy> enemies = new List<Enemy>();

    public Enemy currentEnemy;

    public int enemiesKilled;
    //Enemy Health amount 
    public int currentEnemyHealth;

    //Enemy Damage amount 
    public int currentEnemyDamage;

    public bool isAlive;

    private bool canSpawn = true;

    private SpriteRenderer sr;

    private BeatScroller bs;

    public HealthBarController hc;

    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        bs = FindObjectOfType<BeatScroller>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageEnemy(int damage)
    {
        currentEnemyHealth -= damage;
        //update healthbar 
        hc.UpdateHealthBar(currentEnemyHealth, currentEnemy.healthAmount);

        if(currentEnemyHealth <= 0 && canSpawn)
        {
            currentEnemyHealth = 0;
            EnemyDied();
            canSpawn = false;
        }
    }

    public void SpawnEnemy()
    {
        //instantiate random enemy 
        int index = Random.Range(0, enemies.Count);
        currentEnemy = enemies[index];
        enemies.RemoveAt(index);

        //change Enemy sprite
        sr.sprite = currentEnemy.enemySprite;
        //set health
        currentEnemyHealth = currentEnemy.healthAmount;
        //reset healthbar;
        hc.UpdateHealthBar(currentEnemy.healthAmount, currentEnemyHealth);
        //set damage
        currentEnemyDamage = currentEnemy.damageAmount;
        //set correct damage attack
        bs.enemyNotePb.GetComponent<SpriteRenderer>().sprite = currentEnemy.enemyAttackSprite;
        //
        isAlive = true;
        //turn on sprite renderer
        sr.enabled = true;
        //
        canSpawn = true;
    }

    private void EnemyDied()
    {
        //update killcount
        enemiesKilled++;

        //set to dead
        isAlive = false;
        sr.enabled = false;
        //Find all attacks and notes spawned on screen and destroy them
        GameObject[] allGameObjects = FindObjectsOfType<Transform>().Select(t => t.gameObject).ToArray();
        GameObject[] enemyAttacks = allGameObjects.Where(go => go.CompareTag("Attack") || go.CompareTag("Note")).ToArray();
        foreach (GameObject attack in enemyAttacks)
        {
            Destroy(attack);
        }

        //Check to see how many enemies left 
        if (enemies.Count == 0)
        {
            //end battle sequence
            Debug.Log("LEVEL FINSIHED");
        }
        else 
        {
            //Wait then spawn
            StartCoroutine(WaitAndSpawn());
        }

    }
    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnEnemy();
    }
}
