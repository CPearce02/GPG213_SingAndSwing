using System.Collections;
using System.Collections.Generic;
using Enums;
using Events;
using GameSections.Bard_Abilities.ScriptableObject;
using UnityEngine;

namespace GameSections.Bard_Abilities
{
    public class ComboUIController : MonoBehaviour
    {
        [SerializeField] Transform[] spawnPositions;
        private int index;
        private int noteIndex = 0;

        [SerializeField] public List<GameObject> spawnedNotes = new List<GameObject>();

        private ComboManager cm;

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

        // Start is called before the first frame update
        void Start()
        {
            cm = FindObjectOfType<ComboManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void DisplayComboNotes(Combo combo)
        {
            if (cm.currentEnemy != GetComponentInParent<EnemyPlatforming>() || cm.currentEnemy.canBeDestroyed == true) return;
            foreach (ComboValues value in combo.ComboValues)
            {
                GameObject note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[value], spawnPositions[index].position, Quaternion.identity);
                note.transform.parent = gameObject.transform;
                spawnedNotes.Add(note);
                index++;
            }
            index = 0;
            noteIndex = 0;
        }

        private void UpdateComboNotes(ComboValues value)
        {
            if (noteIndex >= spawnedNotes.Count)
            {
                noteIndex = 0;
            }
            else if (value == spawnedNotes[noteIndex].GetComponent<ComboNoteManager>().value)
            {
                //Correct note - update index and color
                spawnedNotes[noteIndex].GetComponent<SpriteRenderer>().color = Color.green;
                noteIndex++;
            }
            else
            {
                //Incorrect - reset index and flash colors
                noteIndex = 0;
                foreach (GameObject note in spawnedNotes)
                {
                    StartCoroutine(FlashColour(note));
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

        IEnumerator FlashColour(GameObject note)
        {
            note.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.25f);
            note.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
