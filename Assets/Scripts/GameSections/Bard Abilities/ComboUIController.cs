using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Enums;
using System;

public class ComboUIController : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    private int index;
    private int noteIndex = 0;

    [SerializeField] public List<GameObject> spawnedNotes = new List<GameObject>();


    private void OnEnable()
    {
        GameEvents.onNewCombo += DisplayComboNotes;
        GameEvents.onButtonPressed += UpdateComboNotes;
        GameEvents.onComboFinish += ClearComboNotes;
    }

    private void OnDisable()
    {
        GameEvents.onNewCombo -= DisplayComboNotes;
        GameEvents.onButtonPressed -= UpdateComboNotes;
        GameEvents.onComboFinish -= ClearComboNotes;
    }

    private void DisplayComboNotes(Combo combo)
    {
        foreach (ComboValues value in combo.ComboValues)
        {
            GameObject note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[value], spawnPositions[index].position, Quaternion.identity);
            note.transform.parent = gameObject.transform;
            spawnedNotes.Add(note);
            index++;
        }
        index = 0;
    }

    private void UpdateComboNotes(ComboValues value)
    {
        if (noteIndex >= 4)
        {
            noteIndex = 0;
        }
        
        if (value == spawnedNotes[noteIndex].GetComponent<ComboNoteManager>().value)
        {
            //Correct note - update index
            spawnedNotes[noteIndex].GetComponent<SpriteRenderer>().color = Color.green;
            noteIndex++;
        }
        else
        {
            //Incorret - reset index
            foreach (GameObject note in spawnedNotes)
            {
                StartCoroutine(FlashColour(note));
                noteIndex = 0;
            }
        }
    }

    private void ClearComboNotes()
    {
        foreach (GameObject note in spawnedNotes)
        {
            Destroy(note);
        }
        spawnedNotes.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlashColour(GameObject note)
    {
        note.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.25f);
        note.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
