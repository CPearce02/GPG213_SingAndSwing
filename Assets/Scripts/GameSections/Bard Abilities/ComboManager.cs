using System.Collections;
using System.Collections.Generic;
using Events;
using Enums;
using Core.ScriptableObjects;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public Combo currentCombo;
    public EnemyPlatforming currentEnemy;
    private ComboValues expectedNote;
    public int comboIndex = 0 ;
    private bool noArmour;
    private bool hasStarted;

    public float timeFrame = 5f;
    private float sequenceStartTime = 0f;

    private void OnEnable()
    {
        GameEvents.onNewCombo += ComboStart;
        GameEvents.onButtonPressed += CheckComboValue;
        GameEvents.onComboFinish += ComboFinished;
    }

    private void OnDisable()
    {
        GameEvents.onNewCombo -= ComboStart;
        GameEvents.onButtonPressed -= CheckComboValue;
        GameEvents.onComboFinish -= ComboFinished;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) return;
        if (Time.time - sequenceStartTime > timeFrame)
        {
            Debug.Log("Time Up");
            GameEvents.onComboFinish?.Invoke();
        }
    }

    private void CheckComboValue(ComboValues value)
    {
        if (comboIndex == 3)
        {
            //all notes pressed
            noArmour = true;
            GameEvents.onComboFinish?.Invoke();
        }
        else if (value == expectedNote)
        {
            //Increase and set the next note
            comboIndex++;
            expectedNote = currentCombo.ComboValues[comboIndex];
        }
        else
        {
            //Reset Index
            comboIndex = 0;
            expectedNote = currentCombo.ComboValues[comboIndex];
        }
    }
    private void ComboStart(Combo combo)
    {
        //start timer
        sequenceStartTime = Time.time;
        hasStarted = true;
    }

    private void ComboFinished()
    {
        currentCombo = null;
        comboIndex = 0;
        hasStarted = false;
        if(noArmour && currentEnemy != null)
        {
            currentEnemy.canBeDestroyed = true;
        }
        currentEnemy = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentCombo == null)
        {
            currentEnemy = collision.GetComponent<EnemyPlatforming>();
            noArmour = false;
            currentCombo = currentEnemy.enemyData.Combo;
            GameEvents.onNewCombo?.Invoke(currentCombo);
            expectedNote = currentCombo.ComboValues[0];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameEvents.onComboFinish?.Invoke();
        }
    }
}
