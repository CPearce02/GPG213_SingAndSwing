using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    //Scriptable object
    public List<Enemy> enemies = new List<Enemy>();
    //public Enemy[] enemies;
    public Enemy currentEnemy;

    public int enemiesKilled;
    //Enemy Health amount 
    public float currentEnemyHealth;

    private float healthBonus;
    private float damageBonus;
    //Enemy Damage amount 
    public float currentEnemyDamage;

    public bool isAlive;

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

    public void DamageEnemy(float damage)
    {
        currentEnemyHealth -= damage;
        //update healthbar 
        hc.UpdateHealthBar(currentEnemy.healthAmount, currentEnemyHealth);

        if(currentEnemyHealth <= 0)
        {
            EnemyDied();
        }
    }

    public void SpawnEnemy()
    {
        //instantiate random enemy 
        int index = Random.Range(0, enemies.Count);
        currentEnemy = enemies[index];
        //enemies.RemoveAt(index);
        //change Enemy sprite
        sr.sprite = currentEnemy.enemySprite;
        //set health
        currentEnemyHealth = currentEnemy.healthAmount + healthBonus;
        //reset healthbar;
        hc.UpdateHealthBar(currentEnemy.healthAmount, currentEnemyHealth);
        //set damage
        currentEnemyDamage = currentEnemy.damageAmount + damageBonus;
        //set correct damage attack
        bs.enemyNotePb.GetComponent<SpriteRenderer>().sprite = currentEnemy.enemyAttackSprite;
        //set enemy to alive 
        isAlive = true;
        //turn on sprite renderer
        sr.enabled = true;
    }

    private void EnemyDied()
    {
        //ENEMY DIED
        currentEnemyHealth = 0;
        //update killcount
        enemiesKilled++;
        ////increase health bonus
        //healthBonus += 10;
        ////increase damage bonus
        //damageBonus += 5;
        //set to dead
        isAlive = false;
        sr.enabled = false;
        //Find all attacks spawned on screen and destroy them
        GameObject[] enemyAttacks = GameObject.FindGameObjectsWithTag("Attack");
        foreach (GameObject attack in enemyAttacks)
        {
            Destroy(attack);
        }
        //Wait then spawn
        StartCoroutine(WaitAndSpawn());
    }
    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnEnemy();
    }
}
