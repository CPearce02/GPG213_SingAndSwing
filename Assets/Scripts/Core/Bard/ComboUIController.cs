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
        [SerializeField] Transform spawnPosition;
        [SerializeField] Transform endPosition;
        private int _noteIndex = 0;
        private bool _comboStarted;
        private Combo currentCombo;
        
        [Header("State colours")]
        [SerializeField] private Color successColour = new();
        [SerializeField] private Color baseColour = new();
        [SerializeField] private Color failColour = new();

        [SerializeField] public List<SpriteRenderer> spawnedNotes = new ();

        private ComboManager cm;

        void Start()
        {
            cm = FindObjectOfType<ComboManager>();
        }

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

        private void Update()
        {
            if (_comboStarted || spawnedNotes.Count == 0) return;
            if (Vector3.Distance(spawnedNotes[0].gameObject.transform.position, endPosition.position) < 0.1f)
            {
                spawnedNotes[0].gameObject.transform.position = spawnPosition.position;
            }
        }

        private void DisplayComboNotes(Combo combo)
        {
            if (cm.CurrentEnemy != GetComponentInParent<Enemy>() || cm.CurrentEnemy.canBeDestroyed == true) return;
            _comboStarted = false;
            _noteIndex = 0;
            currentCombo = combo;
            SpawnNote(currentCombo.ComboValues[_noteIndex]);
        }

        private void UpdateComboNotes(ComboValues value)
        {
            if (_noteIndex >= spawnedNotes.Count)
            {
                _noteIndex = 0;
            }
            else if (value == spawnedNotes[_noteIndex].GetComponent<ComboNoteManager>().value)
            {
                //Correct note - update index and color and then spawn next note
                spawnedNotes[_noteIndex].GetComponent<SpriteRenderer>().color = successColour;
                _noteIndex++;
                SpawnNote(currentCombo.ComboValues[_noteIndex]);
                _comboStarted = true;

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
            //Stop spawning notes
            currentCombo = null;

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

        private void SpawnNote(ComboValues value)
        {
            //foreach (ComboValues value in combo.ComboValues)
            //{
            //    SpriteRenderer note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[value], spawnPositions[_index].position, Quaternion.identity);
            //    note.transform.parent = gameObject.transform;
            //    spawnedNotes.Add(note);
            //    _index++;
            //}

            SpriteRenderer note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[value], spawnPosition.position, Quaternion.identity);
            note.transform.parent = gameObject.transform;
            spawnedNotes.Add(note);
        }
    }
}
