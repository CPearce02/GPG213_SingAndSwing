using System.Collections;
using System.Collections.Generic;
using Enemies;
using Enums;
using Events;
using UnityEngine;
using Core.ScriptableObjects;

namespace Core.Bard
{
    public class ComboUIControllerOLD : MonoBehaviour
    {
        [SerializeField] public Transform spawnPosition;
        [SerializeField] public Transform endPosition;
        private bool _comboStarted;

        [Header("State colours")]
        private Color _inactiveColour;
        [SerializeField] private Color successColour = new();
        [SerializeField] private Color baseColour = new();
        [SerializeField] private Color failColour = new();

        [Header("Timer")]


        [Header("Notes")]

        private Combo currentCombo;
        [SerializeField] private List<SpriteRenderer> spawnedNotes = new();

        void Start()
        {

        }

        private void OnEnable()
        {
            GameEvents.onNewCombo += (combo) => { currentCombo = combo; SpawnNote(); };
            GameEvents.onButtonPressed += CheckNotePosition;
            GameEvents.onCorrectButtonPressed += DisplayCorrectNote;
            GameEvents.onWrongButtonPressed += DisplayWrongNotes;
            // GameEvents.onComboFinish += ResetComboUI;
        }

        private void OnDisable()
        {
            GameEvents.onNewCombo -= (combo) => {currentCombo = combo; SpawnNote();};
            GameEvents.onButtonPressed -= CheckNotePosition;
            GameEvents.onCorrectButtonPressed -= DisplayCorrectNote;
            GameEvents.onWrongButtonPressed -= DisplayWrongNotes;
            // GameEvents.onComboFinish -= ResetComboUI;
        }

        private void Update()
        {
            
        }

        private void SpawnNote()
        {
            // if (!_canSpawn) return;
            // SpriteRenderer note = Instantiate(ComboDictionary.instance.comboPrefabDictionary[currentCombo.ComboValues[_comboIndex]], _spawnPosition.position, Quaternion.identity);
            // note.transform.parent = currentEnemy.gameObject.transform;
            // spawnedNotes.Add(note);
            // _noteToBePressed = note;
            // foreach (SpriteRenderer _note in spawnedNotes)
            // {
            //     if (_note.TryGetComponent<ComboNoteManager>(out ComboNoteManager _cn))
            //     {
            //         _cn._speed = _increasedSpeed;
            //     }
            // }
            // _canSpawn = false;
        }
        private void ClearSpawnedNotes()
        {
            foreach (SpriteRenderer _note in spawnedNotes)
            {
                Destroy(_note.gameObject);
            }
            spawnedNotes.Clear();
        }
        private void CheckNotePosition(ComboValues comboValues)
        {

        }

        IEnumerator DelaySpawn()
        {
            yield return new WaitForSeconds(1.25f);
            // _canSpawn = true;
        }

        private void DisplayCorrectNote()
        {
            if (spawnedNotes.Count <= 0) return;

            //Change note to success colour
            spawnedNotes[spawnedNotes.Count - 1].color = successColour;
        }

        private void DisplayWrongNotes()
        {
            foreach (SpriteRenderer _note in spawnedNotes)
            {
                StartCoroutine(FlashColour(_note));
            }
        }

        private void ResetComboUI()
        {

        }
       
        IEnumerator FlashColour(SpriteRenderer note)
        {
            note.color = failColour;
            yield return new WaitForSeconds(0.25f);
            note.color = baseColour;
        }
    }
}
