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

        private ComboDictionary _cd;
        private int _comboIndex;

        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private GameObject Notes;
        [SerializeField] private List<Image> spawnedNotes = new();
        private Image _noteToBePressed;

        // Start is called before the first frame update
        void Start()
        {
            _cd = GetComponent<ComboDictionary>();
        }

        private void OnEnable()
        {
            GameEvents.onNewCombo += (combo) => {_currentCombo = combo; SpawnNote();};
            GameEvents.onButtonPressed += CheckValueAndPosition;
        }

        private void OnDisable()
        {
            GameEvents.onNewCombo -= (combo) => { _currentCombo = combo; SpawnNote(); };
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SpawnNote()
        {
            Image note = Instantiate(_cd.comboPrefabDictionary[_currentCombo.ComboValues[_comboIndex]], _spawnPoint.position, Quaternion.identity);
            note.transform.parent = Notes.transform;
            spawnedNotes.Add(note);
            _noteToBePressed = note;
        }

        private void CheckValueAndPosition(ComboValues value)
        {

        }
    }
}
