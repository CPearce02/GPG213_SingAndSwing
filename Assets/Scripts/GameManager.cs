using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Audio")]
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller bS;

    [Header("Health")]
    public float currentPlayerHealth;
    public float maxPlayerHealth;
    public HealthBarController hc;

    [Header("Damage")]
    //public float damagePerNote;
    public float currentDamage;

    [Header("Multiplier")]
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public HealthBarController mc;

    [Header("UI")]
    //public Text damageText;
    public GameObject diedText;
    public GameObject menu;
    public Text slayText;
    // public Text multiplierText;

    [Header("Enemies")]
    public EnemyManager em;


    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        instance = this;
        mc.UpdateHealthBar(3, currentMultiplier); ;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlayerHealth >= maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
        
        if(currentPlayerHealth <= 0)
        {
            currentPlayerHealth = 0;
            slayText.text = "You slayed " + em.enemiesKilled + " creatures";
            StartCoroutine(HeroDied());
        }
    }

    public void StartGame()
    {
        startPlaying = true;
        bS.hasStarted = true;
        music.Play();
        em.SpawnEnemy();
    }

    public void NoteHit(NoteController nC, float damagePerNote)
    {
        //Debug.Log("hit");

        //Determine Multiplier
        CalculateMultiplier();

        //Determine attack type
        //Check if the enemy is weak to the attack type 
        if(nC.nT.ToString() == em.currentEnemy.damageWeak.ToString())
        {
            //set damage 
            currentDamage = damagePerNote * 2;
            //Debug.Log(currentDamage + "double damage");
        }
        // check if the enemy is resistant to the attack type
        else if (nC.nT.ToString() == em.currentEnemy.damageType.ToString())
        {
            currentDamage = damagePerNote / 2;
            //Debug.Log("less damage");
        }
        else
        {
            currentDamage = damagePerNote;
            //Debug.Log("normal damage");
        }

        //Damage Enemy 
        currentDamage = currentDamage * currentMultiplier;
        em.DamageEnemy(currentDamage);
        Debug.Log(currentDamage);
    }

    public void NoteMissed()
    {
        //Damage Player
        Debug.Log("missed");
        currentPlayerHealth -= em.currentEnemyDamage;
        //update player healthbar
        hc.UpdateHealthBar(maxPlayerHealth, currentPlayerHealth);
        ResetMultiplier();
    }
    
    public void HealHero()
    {
        CalculateMultiplier();
        Debug.Log("Healing");
        currentPlayerHealth += 10;
        hc.UpdateHealthBar(maxPlayerHealth, currentPlayerHealth);
    }

    private void CalculateMultiplier()
    {
        //Determine Multiplier
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        //update multiplier bar
        mc.UpdateHealthBar(3, currentMultiplier);
    }

    IEnumerator HeroDied()
    {
        bS.hasStarted = false;
        bS.GetComponent<AudioSource>().enabled = false;
        diedText.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    public void ResetMultiplier()
    {
        //Debug.Log("Multiplier Reset");
        //currentMultiplier = 1;
        //multiplierTracker = 0;
        //mc.UpdateHealthBar(3, currentMultiplier);
    }

    public void EnemyDied()
    {
        bS.beatTempo += 0.25f;
    }
}
