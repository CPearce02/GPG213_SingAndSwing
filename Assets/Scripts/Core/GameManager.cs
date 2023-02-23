using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Core.ScriptableObjects;
using Core.Player;
using Events;
using System.Linq;

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
        mc.UpdateHealthBar(currentMultiplier, 3);
        StartCoroutine("BattleStart");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GameEvents.onSendCameraEvent?.Invoke(Camera.main);
    }

    public void StartBattle()
    {
        startPlaying = true;
        bS.hasStarted = true;
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
        else if (noteDamageType == em.currentEnemy.DamageType.StrongAgainst || noteDamageType == em.currentEnemy.DamageType)
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

    public void PlayerDied()
    {
        ClearScreen();
        music.Stop();
        startPlaying = false;
        bS.hasStarted = false;


        //Check to see how many lives are left 
        if (PlayersManager.instance.warrior.Lives < 0)
        {
            //Send player to level 1 
            StartCoroutine("RestartLevel");
        }
        else
        {
            //ResetBattle
            StartCoroutine("RestartBattle");
        } 
    }

    public void ResetMultiplier()
    {
        Debug.Log("Multiplier Reset");
        currentMultiplier = 1;
        multiplierTracker = 0;
        mc.UpdateHealthBar(currentMultiplier, 3);
    }

    IEnumerator BattleStart()
    {
        music.Play();
        yield return new WaitForSeconds(2);
        StartBattle();
    }
    public void BattleComplete()
    {
        //move on to next platforming section
        //currently just loads the menu
        SceneManager.LoadScene(0);
    }

    IEnumerator RestartBattle()
    {
        diedText.GetComponent<Text>().text = "YOU DIED! Remaining Lives: " + PlayersManager.instance.warrior.Lives;
        diedText.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);
    }
    IEnumerator RestartLevel()
    {
        diedText.GetComponent<Text>().text = "No Remaining Lives";
        diedText.SetActive(true);
        PlayersManager.instance.warrior.ChangeLives(4);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }

    public void ClearScreen()
    {
        //Find all attacks and notes spawned on screen and destroy them
        GameObject[] allGameObjects = FindObjectsOfType<Transform>().Select(t => t.gameObject).ToArray();
        GameObject[] enemyAttacks = allGameObjects.Where(go => go.CompareTag("Attack") || go.CompareTag("Note")).ToArray();
        foreach (GameObject attack in enemyAttacks)
        {
            Destroy(attack);
        }
    }
}
