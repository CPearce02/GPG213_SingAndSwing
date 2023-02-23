using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Core.ScriptableObjects;
using Core.Player;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Audio")]
    public AudioSource music;
    public bool startPlaying;
    public BeatScroller bS;

    //[Header("Damage")]
    //public float damagePerNote;
    private int currentDamage;

    [Header("Multiplier")]
    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public HealthBarController mc;

    [Header("UI")]
    public GameObject diedText;

    [Header("Enemies")]
    public EnemyManager em;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //mc.UpdateHealthBar(3, currentMultiplier); ;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        startPlaying = true;
        bS.hasStarted = true;
        music.Play();
        em.SpawnEnemy();
    }

    public void NoteHit(DamageType noteDamageType, int damagePerNote)
    {
        //Determine Multiplier
        CalculateMultiplier();

        //Determine attack type
        //Check if the enemy is weak to the attack type - DOUBLE DAMAGE
        if (noteDamageType == em.currentEnemy.DamageType.WeaknessAgainst)
        {
            currentDamage = damagePerNote * 2;
        }
        // check if the enemy is resistant to the attack type - HALF DAMAGE
        else if (noteDamageType == em.currentEnemy.DamageType.StrongAgainst)
        {
            currentDamage = damagePerNote / 2;
        }
        // - NORMAL DAMAGE
        else
        {
            currentDamage = damagePerNote;
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
        PlayersManager.instance.DamagePlayer(em.currentEnemyDamage);
        ResetMultiplier();
    }
    public void CalculateMultiplier()
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
        mc.UpdateHealthBar(currentMultiplier, 3);
    }

    //IEnumerator HeroDied()
    //{
    //    bS.hasStarted = false;
    //    bS.GetComponent<AudioSource>().enabled = false;
    //    diedText.SetActive(true);
    //    yield return new WaitForSeconds(5);
    //    SceneManager.LoadScene(0);
    //}

    public void PlayerDied()
    {
        //Check to see how many lives are left 

        //Reset battle 

        //If no more lives left 
        
        //Load scene 1 
    }

    public void ResetMultiplier()
    {
        Debug.Log("Multiplier Reset");
        currentMultiplier = 1;
        multiplierTracker = 0;
        mc.UpdateHealthBar(currentMultiplier, 3);
    }

    public void LevelComplete()
    {

    }
}
