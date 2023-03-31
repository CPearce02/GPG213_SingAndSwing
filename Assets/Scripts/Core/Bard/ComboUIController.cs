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
        private bool _comboStarted;

        [Header("State colours")]
        private Color _inactiveColour;
        [SerializeField] private Color successColour = new();
        [SerializeField] private Color baseColour = new();
        [SerializeField] private Color failColour = new();

        [Header("Timer")]
        public float totalTime;
        private float _initialTotalTime;
        private float _remainingTime;
        private float _initialWidth;
        private float _timerInterval = 0.1f;
        [SerializeField]private SpriteRenderer timerSprite;


        private ComboManager _cm;
        private Enemy _enemy;

        void Start()
        {
            _cm = FindObjectOfType<ComboManager>();
            _enemy = GetComponentInParent<Enemy>();

            _inactiveColour = timerSprite.color;
            _initialTotalTime = totalTime;
            _initialWidth = timerSprite.size.x;
            _remainingTime = totalTime;
        }

        private void OnEnable()
        {
            GameEvents.onCorrectButtonPressed += DisplayCorrectNote;
            GameEvents.onWrongButtonPressed += DisplayWrongNotes;
            GameEvents.onComboFinish += ResetComboUI;
        }

        private void OnDisable()
        {
            GameEvents.onCorrectButtonPressed -= DisplayCorrectNote;
            GameEvents.onWrongButtonPressed -= DisplayWrongNotes;
            GameEvents.onComboFinish -= ResetComboUI;
        }

        private void Update()
        {
            //Don't display combo UI if can be destroyed - UPDATE TO TURN ON UI WHEN ENEMY NEAR BY - TURN OFF WHEN ENEMY NOT WITHIN RANGE?
            if (_enemy.CanBeDestroyed)
            {
                gameObject.SetActive(false);
                ResetComboUI();
                return;
            }
        }

        private void UpdateTimerBar()
        {
            _remainingTime -= _timerInterval;
            float newWidth = (_remainingTime / totalTime) * _initialWidth;
            timerSprite.size = new Vector2(newWidth, timerSprite.size.y);
            if (_remainingTime <= totalTime / 2)
            {
                //If half the timer remaining - set bar to red
                timerSprite.color = failColour;
            }
            else if (_remainingTime <= 0)
            {
                // Timer has reached 0
                CancelInvoke("UpdateTimerBar");
                GameEvents.onComboFinish?.Invoke();
            }
            else
            {
                timerSprite.color = baseColour;
            }
        }
        private void DisplayCorrectNote()
        {
            if (_cm.spawnedNotes.Count <= 0) return;

            //Change note to success colour
            _cm.spawnedNotes[_cm.spawnedNotes.Count - 1].color = successColour;

            //Decrease Time Amount
            if (_comboStarted)
            {
                totalTime--;
                _remainingTime = totalTime;
            }
            //Start Timer
            else
            {
                InvokeRepeating("UpdateTimerBar", _timerInterval, _timerInterval);
                _comboStarted = true;
            }
        }

        private void DisplayWrongNotes()
        {
            foreach (SpriteRenderer _note in _cm.spawnedNotes)
            {
                StartCoroutine(FlashColour(_note));
            }
            ResetTimerBar();
        }

        private void ResetTimerBar()
        {
            timerSprite.color = _inactiveColour;
            totalTime = _initialTotalTime;
            _remainingTime = totalTime;
            float newWidth = (_remainingTime / totalTime) * _initialWidth;
            timerSprite.size = new Vector2(newWidth, timerSprite.size.y);
        }

        private void ResetComboUI()
        {
            _comboStarted = false;
            CancelInvoke("UpdateTimerBar");
            ResetTimerBar();
        }

        IEnumerator FlashColour(SpriteRenderer note)
        {
            note.color = failColour;
            yield return new WaitForSeconds(0.25f);
            note.color = baseColour;
        }
    }
}
