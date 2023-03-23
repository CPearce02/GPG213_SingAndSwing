using Core.ScriptableObjects;
using Enemies;
using Enums;
using Events;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Bard
{
    public class ComboManager : MonoBehaviour
    {
        [SerializeField] private Combo currentCombo;

        public Enemy currentEnemy { get; private set;}
        [SerializeField][ReadOnly] private ComboValues expectedNote;
        [SerializeField] private int _comboIndex;
        private bool _noArmour;


        [Header("Spawned Notes")]
        [SerializeField] public List<SpriteRenderer> spawnedNotes = new();
        private Transform _spawnPosition;
        private Transform _endPosition;
        private float _increasedSpeed = 2f;

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
            if(Vector2.Distance(transform.position, currentEnemy.gameObject.transform.position) > 7)
            {
                GameEvents.onComboFinish?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Get the current enemy and set the spawn position
            if (collision.TryGetComponent<Enemy>(out Enemy enemyComponent) && currentCombo == null)
            {
                SetCurrentEnemy(enemyComponent);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                //enemy can move out of range, find alternative to just ending the combo? 
                //GameEvents.onComboFinish?.Invoke();
            }
        }

        private void CheckComboValue(ComboValues value)
        {
            if (value == expectedNote)
            {
                //Increase and set the next note
                _comboIndex++;
                if (_comboIndex >= currentCombo.ComboValues.Count)
                {
                    //All notes pressed correctly - Combo Finished 
                    _noArmour = true;
                    GameEvents.onComboFinish?.Invoke();
                }
                else
                {
                    //Correct Note Pressed - Update UI, Spawn Next Note & Increase Speed
                    GameEvents.onCorrectButtonPressed?.Invoke();
                    _increasedSpeed += 2f;
                    SpawnNote(_comboIndex);
                    expectedNote = currentCombo.ComboValues[_comboIndex];
                }
            }
            else
            {
                //Incorrect Note pressed
                GameEvents.onWrongButtonPressed?.Invoke();
                //Reset Index and Speed
                _comboIndex = 0;
                _increasedSpeed = 2f;
                expectedNote = currentCombo.ComboValues[_comboIndex];

                SpawnNote(_comboIndex);
            }
            //Debug.Log(comboIndex);
        }

        private void ComboStart(Combo combo)
        {
            //Start timer


            //Set index
            _comboIndex = 0;

            //Spawn the first note 
            SpawnNote(0);
        }

        private void ComboFinished()
        {
            ClearSpawnedNotes();

            //Check to see if combo is complete 
            if (_noArmour && currentEnemy != null)
            {
                //Enemy can now take damage
                currentEnemy.CanBeDestroyed = true;
            }

            //Clear enemy data and spawn position
            currentCombo = null;
            currentEnemy = null;
            _spawnPosition = null; 
            _endPosition = null; 
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
            if(_spawnPosition == null)
            {
                _spawnPosition = currentEnemy.gameObject.GetComponentInChildren<ComboUIController>().spawnPosition;
                _endPosition = currentEnemy.gameObject.GetComponentInChildren<ComboUIController>().endPosition;
            }

            BeatListener _bl = GetComponent<BeatListener>();
            _bl.onBeatEvent.AddListener(SpawnOnBeat);
            
            //Start Combo
            GameEvents.onNewCombo?.Invoke(currentCombo);
        }

        private void SpawnNote(int index)
        {
            SpriteRenderer note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[currentCombo.ComboValues[index]], _spawnPosition.position, Quaternion.identity);
            spawnedNotes.Add(note);
            foreach (SpriteRenderer _note in spawnedNotes)
            {
                _note.GetComponent<ComboNoteManager>()._speed = _increasedSpeed;
            }
        }

        public void SpawnOnBeat()
        {

        }

        private void ClearSpawnedNotes()
        {
            foreach (SpriteRenderer _note in spawnedNotes)
            {
                Destroy(_note.gameObject);
            }
        }
    }
}