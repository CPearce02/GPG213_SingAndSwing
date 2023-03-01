using System.Collections;
using System.Collections.Generic;
using Events;
using Enums;
using Core.ScriptableObjects;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public Enemy currentEnemy;
    [SerializeField] private List<ComboValues> currentCombo = new List<ComboValues>();
    private ComboValues expectedNote;

    private void OnEnable()
    {
        GameEvents.onNewCombo += ComboStart;
        GameEvents.onButtonPressed += CheckComboValue;
    }

    private void OnDisable()
    {
        GameEvents.onNewCombo -= ComboStart;
        GameEvents.onButtonPressed -= CheckComboValue;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckComboValue(ComboValues value)
    {

    }
    private void ComboStart()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentEnemy = collision.GetComponent<EnemyManager>().currentEnemy;
            currentCombo.AddRange(currentEnemy.ComboData);
        }
    }
}
