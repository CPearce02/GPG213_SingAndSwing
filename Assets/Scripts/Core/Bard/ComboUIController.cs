using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableObjects;
using Events;
using UnityEngine.UI;
using Enums;

namespace Core.Bard
{
    public class ComboUIController : MonoBehaviour
    {
        [SerializeField] private Combo _currentCombo;
        private int _comboIndex;

        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private RectTransform _hitBox;
        [SerializeField] private GameObject Notes;
        [SerializeField] private List<Image> spawnedNotes = new();

        [Header("State colours")]
        [SerializeField] private Color successColour = new();
        [SerializeField] private Color baseColour = new();
        [SerializeField] private Color failColour = new();
        
        private Image _noteToBePressed;
        private ComboValues _expectedNote;

        // private bool _noArmour;

        // Start is called before the first frame update
        void Start()
        {
        }

        private void OnEnable()
        {
            GameEvents.onNewCombo += (combo) => {_currentCombo = combo; SpawnNote();};
            GameEvents.onButtonPressed += CheckValueAndPosition;
            GameEvents.onComboFinish +=  CheckComboComplete;
        }

        private void OnDisable()
        {
            GameEvents.onNewCombo -= (combo) => { _currentCombo = combo; SpawnNote(); };
            GameEvents.onButtonPressed -= CheckValueAndPosition;
            GameEvents.onComboFinish += CheckComboComplete;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void DebugHitBox()
        {   
            if (_noteToBePressed == null) return;
            //Check Position
            if (Vector2.Distance(_noteToBePressed.gameObject.GetComponent<RectTransform>().position, _hitBox.position) < 55)
            {
                Debug.Log("CAN HIT");
                _noteToBePressed.color = Color.green;
            }
            else
            {
                _noteToBePressed.color = Color.red;
            }
        }
        private void SpawnNote()
        {
            Image note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[_currentCombo.ComboValues[_comboIndex]], _spawnPoint.position, Quaternion.identity);
            note.transform.SetParent(Notes.transform);
            spawnedNotes.Add(note);
            _noteToBePressed = note;
            _expectedNote = _currentCombo.ComboValues[_comboIndex];
        }

        private void CheckValueAndPosition(ComboValues value)
        {
            //Correct Value
            if(value == _expectedNote)
            {
                //Check Position - Is it within hitbox?
                if(Vector2.Distance(_noteToBePressed.gameObject.GetComponent<RectTransform>().position,_hitBox.position) < 55)
                {
                    //Correct 
                    GameEvents.onCorrectButtonPressed?.Invoke();
                    IncreaseComboIndex();
                }
                else
                {
                    //Incorrect Postion
                    // GameEvents.onWrongButtonPressed?.Invoke();
                    DisplayWrongNotes();
                }
            }
            //Incorrect Value
            else
            {
                DisplayWrongNotes();
                // GameEvents.onComboFinish?.Invoke(false);
            }
        }

        private void IncreaseComboIndex()
        {
            _comboIndex++;
            _noteToBePressed.color = successColour;
            _noteToBePressed.GetComponent<ComboNoteManager>()._beenPressed = true;

            if (_comboIndex >= _currentCombo.ComboValues.Count)
            {
                //All notes pressed correctly - Combo Finished 
                GameEvents.onComboFinish?.Invoke(true);
            }
            else
            {
                //Correct Note Pressed - Update UI, Spawn Next Note, Increase Speed, DecreaseTimer;

                //_increasedSpeed += 2f;
                SpawnNote();
            }
        }

        private void ResetCombo()
        {
            //Remove all spawned notes
            ClearSpawnedNotes();
            //Reset Combo index 
            _comboIndex = 0;

        }
        private void ClearSpawnedNotes()
        {
            foreach (Image _note in spawnedNotes)
            {
                if(_note != null)
                {
                    Destroy(_note.gameObject);
                }
            }
            spawnedNotes.Clear();
        }

        private void CheckComboComplete(bool complete)
        {
            if(complete)
            {
                
            }
            else
            {
                DisplayWrongNotes();
            }
        }

        private void DisplayWrongNotes(){
            foreach (Image _note in spawnedNotes)
            {
                if(_note == null) return;
                StartCoroutine(FlashColour(_note));
            }
            StartCoroutine(DelaySpawn());
        }

        IEnumerator FlashColour(Image note)
        {
            note.color = failColour;
            yield return new WaitForSeconds(0.5f);
            note.color = baseColour;
        }

        IEnumerator DelaySpawn()
        {
            yield return new WaitForSeconds(1f);
            ResetCombo();
            SpawnNote();
        }
    }
}
