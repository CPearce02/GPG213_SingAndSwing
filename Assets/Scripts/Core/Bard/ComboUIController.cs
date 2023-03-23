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
    public class ComboUIController : MonoBehaviour
    {
        [SerializeField] public Transform spawnPosition;
        [SerializeField] public Transform endPosition;
        private int _noteIndex = 0;
        private bool _comboStarted;
        private ComboValues _noteToSpawn;
        private Combo _currentCombo;
        
        [Header("State colours")]
        [SerializeField] private Color successColour = new();
        [SerializeField] private Color baseColour = new();
        [SerializeField] private Color failColour = new();

        private ComboManager _cm;
        private Enemy _enemy;

        void Start()
        {
            _cm = FindObjectOfType<ComboManager>();
            _enemy = GetComponentInParent<Enemy>();
        }

        private void OnEnable()
        {
            GameEvents.onCorrectButtonPressed += DisplayCorrectNote;
            GameEvents.onWrongButtonPressed += DisplayWrongNotes;
        }

        private void OnDisable()
        {
            GameEvents.onCorrectButtonPressed -= DisplayCorrectNote;
            GameEvents.onWrongButtonPressed += DisplayWrongNotes;
        }

        private void Update()
        {
            //Don't display combo UI if can be destroyed
            if (_enemy.CanBeDestroyed)
            {
                gameObject.SetActive(false);
                return;
            }
        }

        private void DisplayCorrectNote()
        {
            _cm.spawnedNotes[_cm.spawnedNotes.Count - 1].color = successColour;
        }

        private void DisplayWrongNotes()
        {
            foreach (SpriteRenderer _note in _cm.spawnedNotes)
            {
                StartCoroutine(FlashColour(_note));
            }
        }
        IEnumerator FlashColour(SpriteRenderer note)
        {
            note.color = failColour;
            yield return new WaitForSeconds(0.25f);
            note.color = baseColour;
        }
    }
}
