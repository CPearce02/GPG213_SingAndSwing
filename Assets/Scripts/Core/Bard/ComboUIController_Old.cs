using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Core.ScriptableObjects;
using Enemies;
using Enums;
using Events;
using UnityEngine;

namespace Core.Bard
{
    public class ComboUIController_Old : MonoBehaviour
    {
        [SerializeField] Transform spawnPosition;
        [SerializeField] Transform endPosition;
        private int _noteIndex = 0;
        private bool _comboStarted;
        private ComboValues _noteToSpawn;
        private Combo _currentCombo;
        
        [Header("State colours")]
        [SerializeField] private Color successColour = new();
        [SerializeField] private Color baseColour = new();
        [SerializeField] private Color failColour = new();

        [SerializeField] public List<SpriteRenderer> spawnedNotes = new ();

        private ComboManager _cm;
        private Enemy _enemy;
        private bool _canBeSpawned;
        private float _increasedSpeed;

        void Start()
        {
            _cm = FindObjectOfType<ComboManager>();
            _enemy = GetComponentInParent<Enemy>();
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
            //Don't display combo UI if can be destroyed
            if (_enemy.CanBeDestroyed)
            {
                gameObject.SetActive(false);
                return;
            }

            if (_comboStarted || spawnedNotes.Count == 0) 
            {
                for (int i = 0; i < spawnedNotes.Count; i++)
                {
                    if (Vector3.Distance(spawnedNotes[i].gameObject.transform.position, endPosition.position) < 0.1f)
                    {
                        spawnedNotes[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                SlowDownFirstNote();
            }

        }

        private void DisplayComboNotes(Combo combo)
        {
            if (_cm.currentEnemy != GetComponentInParent<Enemy>() || _cm.currentEnemy.CanBeDestroyed == true) return;
            _comboStarted = false;
            _noteIndex = 0;
            _currentCombo = combo;
            SpawnNote(_currentCombo.ComboValues[_noteIndex]);
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
                if (_noteIndex >= _currentCombo.ComboValues.Count) return;
                SpawnNote(_currentCombo.ComboValues[_noteIndex]);
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
            _currentCombo = null;
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
            ClearComboNotes();
        }

        private void SlowDownFirstNote()
        {
            //Start Timer - Once the first note reaches the end of the bar then end combo
            //spawnedNotes[0].GetComponent<ComboNoteManager>()._speed = 2f;
            if (Vector3.Distance(spawnedNotes[0].gameObject.transform.position, endPosition.position) < 0.1f)
            {
                //spawnedNotes[0].gameObject.transform.position = spawnPosition.position;

                //timer complete
                GameEvents.onComboFinish?.Invoke();
            }
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
            _increasedSpeed ++;
            _noteToSpawn = value;
            //Debug.Log(value);
        }

        public void SpawnOnBeat()
        {
            if (_cm.currentEnemy != GetComponentInParent<Enemy>() || _cm.currentEnemy.CanBeDestroyed == true || _currentCombo == null) return;

            //check to see if note is already spawned and update speed
            foreach (SpriteRenderer note in spawnedNotes)
            {
                //note.GetComponent<ComboNoteManager>()._speed += _increasedSpeed;

                if (_noteToSpawn == note.GetComponent<ComboNoteManager>().value)
                {
                    _canBeSpawned = false;
                }
                else
                {
                    _canBeSpawned = true;
                }
            }

            //if not then spawn it
            if(spawnedNotes.Count == 0 || _canBeSpawned)
            {
                SpriteRenderer note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[_noteToSpawn], spawnPosition.position, Quaternion.identity);
                note.transform.parent = gameObject.transform;
                spawnedNotes.Add(note);
                return;
            }

        }
    }
}
