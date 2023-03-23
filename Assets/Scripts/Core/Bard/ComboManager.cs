using Core.ScriptableObjects;
using Enemies;
using Enums;
using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Bard
{
    public class ComboManager : MonoBehaviour
    {
        private BeatListener _bl;
        
        [Header("Enemy Data")]
        [SerializeField] private Combo currentCombo;
        [SerializeField][ReadOnly] private ComboValues expectedNote;
        [SerializeField] private int _comboIndex;
        private bool _noArmour;
        public Enemy currentEnemy { get; private set; }

        [Header("Notes")]
        [SerializeField] public List<SpriteRenderer> spawnedNotes = new();
        [SerializeField] private float _originalSpeed;
        private SpriteRenderer _noteToBePressed;
        private Transform _spawnPosition;
        private Transform _endPosition;
        private float _increasedSpeed;
        private bool _canSpawn = true;

        private void Start()
        {
            _increasedSpeed = _originalSpeed;
            _bl = GetComponent<BeatListener>();
        }
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

        // Update is called once per frame
        void Update()
        {
            if (currentEnemy == null) return;
            //If enemy is out of range - EndCombo
            if (Vector2.Distance(transform.position, currentEnemy.gameObject.transform.position) >= 10f)
            {
                GameEvents.onComboFinish?.Invoke();
            }

            //Destroy notes outside of bar
            if (spawnedNotes.Count >= 0)
            {
                for (int i = 0; i < spawnedNotes.Count; i++)
                {
                    if (Vector3.Distance(spawnedNotes[i].gameObject.transform.position, _endPosition.position) < 0.1f)
                    {
                        Destroy(spawnedNotes[i].gameObject);
                        spawnedNotes.RemoveAt(i);
                    }
                }
            }

            //IF note to be pressed reaches end without being pressed then END COMBO
            if (_noteToBePressed != null && (Vector3.Distance(_noteToBePressed.gameObject.transform.position, _endPosition.position) < 0.1f))
            {
                GameEvents.onComboFinish?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Set the current enemy and set the spawn position
            if (collision.TryGetComponent<Enemy>(out Enemy enemyComponent) && currentCombo == null)
            {
                if(!enemyComponent.CanBeDestroyed) SetCurrentEnemy(enemyComponent);
            }
        }

        private void CheckComboValue(ComboValues value)
        {
            if (value == expectedNote)
            {
                //Increase and set the next note
                _comboIndex++;
                            _noteToBePressed = null;

                if (_comboIndex >= currentCombo.ComboValues.Count)
                {
                    //All notes pressed correctly - Combo Finished 
                    _noArmour = true;
                    GameEvents.onComboFinish?.Invoke();
                }
                else
                {
                    //Correct Note Pressed - Update UI, Spawn Next Note, Increase Speed, DecreaseTimer;
                    GameEvents.onCorrectButtonPressed?.Invoke();
                    _increasedSpeed += 2f;
                    _canSpawn = true;
                    expectedNote = currentCombo.ComboValues[_comboIndex];
                }
            }
            else
            {
                //Incorrect Note pressed
                GameEvents.onWrongButtonPressed?.Invoke();
                //Reset Index, Speed & Timer
                _comboIndex = 0;
                _increasedSpeed = _originalSpeed;
                _noteToBePressed = null;
                expectedNote = currentCombo.ComboValues[_comboIndex];
                StartCoroutine("DelaySpawn");
            }
            //Debug.Log(comboIndex);
        }

        private void ComboStart(Combo combo)
        {
            //Set index
            _comboIndex = 0;

            //Spawn the first note 
            SpawnNote();
        }

        private void ComboFinished()
        {
            //Check to see if combo is complete 
            if (_noArmour && currentEnemy != null)
            {
                //Set can be destory method to true 
                currentEnemy.SetCanBeDestroyed(true);
            }

            //Clear enemy data and spawn position
            _comboIndex = 0;
            currentCombo = null;
            currentEnemy = null;
            _spawnPosition = null; 
            _endPosition = null;
            _noteToBePressed = null;
            _canSpawn = true;

            //Reset speed
            _increasedSpeed = _originalSpeed;

            ClearSpawnedNotes();

            _bl.onBeatEvent.RemoveListener(SpawnNote);
        }

        private void SetCurrentEnemy(Enemy _enemyComponent)
        {
            //Set the enemy data
            currentEnemy = _enemyComponent;
            _noArmour = false;
            if (_enemyComponent.enemyData == null) return;
            currentCombo = currentEnemy.enemyData.Combo;
            expectedNote = currentCombo.ComboValues[0];

            //Set the spawn position for the notes
            if (_spawnPosition != null) return;
            _spawnPosition = currentEnemy.gameObject.GetComponentInChildren<ComboUIController>().spawnPosition;
            _endPosition = currentEnemy.gameObject.GetComponentInChildren<ComboUIController>().endPosition;

            _bl.onBeatEvent.AddListener(SpawnNote);

            //Start Combo
            GameEvents.onNewCombo?.Invoke(currentCombo);
        }

        /// <summary>
        /// Spawn on next available beat
        /// </summary>
        private void SpawnNote()
        {
            if (!_canSpawn) return;
            SpriteRenderer note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[currentCombo.ComboValues[_comboIndex]], _spawnPosition.position, Quaternion.identity);
            note.transform.parent = currentEnemy.gameObject.transform;
            spawnedNotes.Add(note);
            _noteToBePressed = note;
            foreach (SpriteRenderer _note in spawnedNotes)
            {
                if (_note.TryGetComponent<ComboNoteManager>(out ComboNoteManager _cn))
                {
                    _cn._speed = _increasedSpeed;
                }
            }
            _canSpawn = false;
        }

        private void ClearSpawnedNotes()
        {
            foreach (SpriteRenderer _note in spawnedNotes)
            {
                Destroy(_note.gameObject);
            }
            spawnedNotes.Clear();
        }

        IEnumerator DelaySpawn()
        {
            yield return new WaitForSeconds(1f);
            _canSpawn = true;
        } 
    }
}