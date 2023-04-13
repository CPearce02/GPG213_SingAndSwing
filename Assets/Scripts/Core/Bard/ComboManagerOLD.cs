using Core.ScriptableObjects;
using Enemies;
using Enums;
using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Bard
{
    public class ComboManagerOLD : MonoBehaviour
    {
        private BeatListener _bl;
        
        private AudioSource _au;

        [Header("Audio")]
        [SerializeField] private AudioClip _guitarChord;
        [SerializeField] private AudioClip _failGuitarChord;
        private float _originalPitch;

        
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
            _au = GetComponent<AudioSource>();
            _originalPitch = _au.pitch;
        }
        private void OnEnable()
        {
            GameEvents.onButtonPressed += CheckComboValue;
            // GameEvents.onComboFinish += ComboFinished;
        }

        private void OnDisable()
        {
            GameEvents.onButtonPressed -= CheckComboValue;
            // GameEvents.onComboFinish -= ComboFinished;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentEnemy == null) return;

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
                // GameEvents.onComboFinish?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Set the current enemy and set the spawn position
            if (collision.TryGetComponent<Enemy>(out Enemy enemyComponent) && currentCombo == null)
            {
                if(!enemyComponent.CanBeDestroyed && enemyComponent.enemyData != null) SetCurrentEnemy(enemyComponent);
            }
        }

        private void CheckComboValue(ComboValues value)
        {
            if(currentEnemy == null) return;
            if (value == expectedNote)
            {
                //Increase and set the next note
                _comboIndex++;
                _noteToBePressed = null;
                GameEvents.onCorrectButtonPressed?.Invoke();
                if(_comboIndex == 1) _au.pitch = _originalPitch;
                PlayGuitar();

                if (_comboIndex >= currentCombo.ComboValues.Count)
                {
                    //All notes pressed correctly - Combo Finished 
                    _noArmour = true;
                    // GameEvents.onComboFinish?.Invoke();
                }
                else
                {
                    //Correct Note Pressed - Update UI, Spawn Next Note, Increase Speed, DecreaseTimer;
                    _increasedSpeed += 2f;
                    _canSpawn = true;
                    expectedNote = currentCombo.ComboValues[_comboIndex];
                }
            }
            else
            {
                //Incorrect Note pressed
                GameEvents.onWrongButtonPressed?.Invoke();
                PlayFailGuitar();

                //Reset Index, Speed & Timer
                _comboIndex = 0;
                _increasedSpeed = _originalSpeed;
                _noteToBePressed = null;
                expectedNote = currentCombo.ComboValues[_comboIndex];
                StartCoroutine("DelaySpawn");
            }
            //Debug.Log(comboIndex);
        }

        private void ComboFinished()
        {
            if (currentEnemy == null) return;
            //Check to see if combo is complete 
            if (_noArmour && currentEnemy != null)
            {
                //Set can be destory method to true 
                currentEnemy.SetCanBeDestroyed(true);
            }
            else
            {
                PlayFailGuitar();
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


        }

        private void SetCurrentEnemy(Enemy _enemyComponent)
        {
            //Set the enemy data
            if (_enemyComponent.enemyData == null) return;
            currentEnemy = _enemyComponent;
            _noArmour = false;
            currentCombo = currentEnemy.enemyData.Combo;
            expectedNote = currentCombo.ComboValues[0];


            //Start Combo
            GameEvents.onNewCombo?.Invoke(currentCombo);
        }



        private void PlayGuitar()
        {
            _au.clip = _guitarChord;
            _au.pitch += 0.5f;
            _au.Play();
        }

        private void PlayFailGuitar()
        {
            _au.clip = _failGuitarChord;
            _au.pitch = 1f;
            _au.Play();
        }
    }
}