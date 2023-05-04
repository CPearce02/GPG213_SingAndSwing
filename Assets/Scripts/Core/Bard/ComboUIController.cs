using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableObjects;
using DG.Tweening;
using Events;
using UnityEngine.UI;
using Enums;
using TMPro;

namespace Core.Bard
{
    public class ComboUIController : MonoBehaviour
    {
        [SerializeField][ReadOnly] private Combo _currentCombo;
        private int _comboIndex;

        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private RectTransform _hitBox;
        [SerializeField] private GameObject Notes;
        [SerializeField] private List<Image> spawnedNotes = new();

        [Header("State colours")]
        [SerializeField] private Color successColour = new();
        // [SerializeField] private Color baseColour = new();
        // [SerializeField] private Color failColour = new();

        [Header("Speed")]

        private float _originalSpeed = 3.25f;
        private float _increasedSpeed;

        private Image _noteToBePressed;
        private ComboValues _expectedNote;


        // Start is called before the first frame update
        void Start()
        {
            _increasedSpeed = _originalSpeed;
        }

        private void OnEnable()
        {
            GameEvents.onNewCombo += SetCombo;
            GameEvents.onButtonPressed += CheckValueAndPosition;
            GameEvents.onComboFinish += CheckComboComplete;
        }

        private void OnDisable()
        {
            GameEvents.onNewCombo -= SetCombo;
            GameEvents.onButtonPressed -= CheckValueAndPosition;
            GameEvents.onComboFinish -= CheckComboComplete;
        }
        private void DebugHitBox()
        {
            if (_noteToBePressed == null) return;
            //Check Position
            if (Vector2.Distance(_noteToBePressed.gameObject.GetComponent<RectTransform>().position, _hitBox.position) < 55)
            {
                // Debug.Log("CAN HIT");
                _noteToBePressed.color = Color.green;
            }
            else
            {
                _noteToBePressed.color = Color.red;
            }
        }

        private void SetCombo(Combo combo)
        {
            _currentCombo = combo;
            ResetCombo();
            SpawnNote();
        }

        private void SpawnNote()
        {
            if (_currentCombo == null) return;
            Image note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[_currentCombo.ComboValues[_comboIndex]], _spawnPoint.position, Quaternion.identity);
            note.transform.SetParent(Notes.transform);
            note.transform.localScale = new Vector3(1, 1, 1);
            spawnedNotes.Add(note);
            _noteToBePressed = note;
            _noteToBePressed.GetComponent<ComboNoteManager>().SetMoveDuration(_increasedSpeed);
            _expectedNote = _currentCombo.ComboValues[_comboIndex];
        }

        private void CheckValueAndPosition(ComboValues value)
        {
            if (_currentCombo == null) return;

            //Correct Value
            if (value == _expectedNote)
            {
                //Check Position - Is it within hitbox?
                // if(Vector2.Distance(_noteToBePressed.gameObject.GetComponent<RectTransform>().position,_hitBox.position) < 55)
                if (RectTransformUtility.RectangleContainsScreenPoint(_hitBox, _noteToBePressed.gameObject.GetComponent<RectTransform>().position))
                {
                    //Correct 
                    GameEvents.onCorrectButtonPressed?.Invoke();
                    IncreaseComboIndex();
                }
                else
                {
                    //Incorrect Postion
                    GameEvents.onComboFinish?.Invoke(false);
                    GameEvents.onWrongButtonPressed?.Invoke();
                }
            }
            //Incorrect Value
            else
            {
                GameEvents.onComboFinish?.Invoke(false);
                GameEvents.onWrongButtonPressed?.Invoke();
            }
        }

        private void IncreaseComboIndex()
        {
            _comboIndex++;
            _noteToBePressed.color = successColour;
            _noteToBePressed.GetComponentInChildren<TextMeshProUGUI>().color = successColour;
            _noteToBePressed.GetComponent<ComboNoteManager>()._beenPressed = true;

            if (_comboIndex >= _currentCombo.ComboValues.Count)
            {
                //All notes pressed correctly - Combo Finished 
                GameEvents.onComboFinish?.Invoke(true);
            }
            else
            {
                //Correct Note Pressed - Spawn Next Note, Increase Speed
                _increasedSpeed -= 0.5f;
                SpawnNote();
            }
        }

        private void ClearSpawnedNotes()
        {
            foreach (Image _note in spawnedNotes)
            {
                if (_note != null)
                {
                    Destroy(_note.gameObject);
                }
            }
            spawnedNotes.Clear();
        }

        private void CheckComboComplete(bool complete)
        {
            // if(complete)
            // {
            //     _comboIndex = 0;
            //     if(_currentCombo == null)
            //     {
            //         ClearSpawnedNotes();
            //     }
            // }
            // else
            // {

            // }
            ResetCombo();
        }

        private void ResetCombo()
        {
            //Remove all spawned notes
            if (spawnedNotes != null) ClearSpawnedNotes();
            //Reset Combo index 
            _comboIndex = 0;
            //Reset Speed;
            _increasedSpeed = _originalSpeed;
        }

    }
}
