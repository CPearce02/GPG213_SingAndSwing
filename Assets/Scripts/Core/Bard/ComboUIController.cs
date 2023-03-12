using System.Collections;
using System.Collections.Generic;
using Core.ScriptableObjects;
using Enemies;
using Enums;
using Events;
using UnityEngine;

namespace Core.Bard
{
    public class ComboUIController : MonoBehaviour
    {
        [SerializeField] Transform[] spawnPositions;
        private int _index;
        private int _noteIndex = 0;
        
        [Header("State colours")]
        [SerializeField] private Color successColour = new();
        [SerializeField] private Color baseColour = new();
        [SerializeField] private Color failColour = new();

        [SerializeField] public List<SpriteRenderer> spawnedNotes = new ();

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

        void Start()
        {
            cm = FindObjectOfType<ComboManager>();
        }

        private void DisplayComboNotes(Combo combo)
        {
            if (cm.CurrentEnemy != GetComponentInParent<Enemy>() || cm.CurrentEnemy.canBeDestroyed == true) return;
            foreach (ComboValues value in combo.ComboValues)
            {
                SpriteRenderer note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[value], spawnPositions[_index].position, Quaternion.identity);
                note.transform.parent = gameObject.transform;
                spawnedNotes.Add(note);
                _index++;
            }
            _index = 0;
            _noteIndex = 0;
        }

        private void UpdateComboNotes(ComboValues value)
        {
            if (_noteIndex >= spawnedNotes.Count)
            {
                _noteIndex = 0;
            }
            else if (value == spawnedNotes[_noteIndex].GetComponent<ComboNoteManager>().value)
            {
                //Correct note - update index and color
                spawnedNotes[_noteIndex].GetComponent<SpriteRenderer>().color = successColour;
                _noteIndex++;
            }
            else
            {
                //Incorrect - reset index and flash colors
                _noteIndex = 0;
                foreach (SpriteRenderer note in spawnedNotes)
                {
                    StartCoroutine(FlashColour(note));
                }
            }
        }
    
        private void ClearComboNotes()
        {
            foreach (SpriteRenderer note in spawnedNotes)
            {
                Destroy(note.gameObject);
            }
            spawnedNotes.Clear();
        }

        IEnumerator FlashColour(SpriteRenderer note)
        {
            note.color = failColour;
            yield return new WaitForSeconds(0.25f);
            note.color = baseColour;
        }
    }
}
