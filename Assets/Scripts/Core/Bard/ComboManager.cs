using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Events;
using Core.ScriptableObjects;
using Effects;

namespace Core.Bard
{
    public class ComboManager : MonoBehaviour
    {
        private Enemy _currentEnemy;
        [SerializeField][ReadOnly] private Combo _currentEnemyCombo;
        public List<Enemy> _enemies = new List<Enemy>();
        [SerializeField][ReadOnly] private int _enemyListIndex = -1;

        private AudioSource _au;

        [Header("Audio")]
        [SerializeField] private AudioClip _guitarChord;
        [SerializeField] private AudioClip _failGuitarChord;
        private float _originalPitch;
        private bool _pitchSet;

        private int _colourIndex;

        // Start is called before the first frame update
        void Start()
        {
            _au = GetComponent<AudioSource>();
            _originalPitch = _au.pitch;
        }

        private void OnEnable()
        {
            GameEvents.onComboFinish += ComboFinished;
            GameEvents.onCorrectButtonPressed += PlayGuitar;
            GameEvents.onWrongButtonPressed += PlayFailGuitar;
        }        

        private void OnDisable()
        {
            GameEvents.onComboFinish -= ComboFinished;
            GameEvents.onCorrectButtonPressed -= PlayGuitar;
            GameEvents.onWrongButtonPressed -= PlayFailGuitar;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemyComponent) && enemyComponent.enemyData != null )
            {
                //Check if the enemy has a combo, isn't already within the enemies list and if the enemy has a shield. 
                if (enemyComponent.enemyData.Combo != null && !_enemies.Contains(enemyComponent) && !enemyComponent.CanBeDestroyed)
                {
                    _enemies.Add(enemyComponent);
                    // if(_currentEnemy == null) SelectCurrent(enemyComponent);
                }
            }
        }

        private void ComboFinished(bool completed)
        {
            if(completed)
            {
                //Turn off sheild 
                _currentEnemy.SetCanBeDestroyed(true);
                //Remove from list 
                _enemies.Remove(_currentEnemy);
                _currentEnemy = null;
                //Highlight next enemy
                SendEnemy();
                //Reset Pitch
                _pitchSet = false;
            }
        }

        public void SendEnemy()
        {
            // If there are no enemies, return
            if (_enemies.Count == 0) return;

            // If there is no selected enemy, select the first one
            if (_enemyListIndex == -1)
            {
                SelectCurrent(_enemies[0]);
                return;
            }

            // Deselect the current enemy
            DeselectCurrent();

            // Increment the selected index
            _enemyListIndex++;

            // Wrap around if we reached the end of the list
            if (_enemyListIndex >= _enemies.Count)
            {
                _enemyListIndex = 0;
            }

            // Highlight the new selected enemy
            SelectCurrent(_enemies[_enemyListIndex]);
        }

        private void DeselectCurrent()
        {
            if (_currentEnemy != null)
            {
                _colourIndex = 0;
                _currentEnemy.GetComponentInChildren<ShieldHandler>().ChangeColour(_colourIndex);
                _currentEnemy = null;
            }
        }

        private void SelectCurrent(Enemy _enemy)
        {
            //Set index
            _enemyListIndex = _enemies.IndexOf(_enemy);
            //Set Current Enemy
            _currentEnemy = _enemy;
            //Highlight Enemy
            _colourIndex = 0;
            _currentEnemy.GetComponentInChildren<ShieldHandler>().ChangeColour(_colourIndex);
            GameEvents.onTargetEnemyEvent?.Invoke(_enemy);
            //Send the Combo to UI
            GameEvents.onNewCombo?.Invoke(_enemy.enemyData.Combo);
        }

        private void PlayGuitar()
        {
            if(!_pitchSet) {_au.pitch = _originalPitch; _pitchSet = true;}
            _au.clip = _guitarChord;
            _au.pitch += 0.5f;
            _au.Play();

            _colourIndex++;
            _currentEnemy.GetComponentInChildren<ShieldHandler>().ChangeColour(_colourIndex);

        }

        private void PlayFailGuitar()
        {
            _pitchSet = false;
            _au.clip = _failGuitarChord;
            _au.pitch = 1f;
            _au.Play();

            _colourIndex = 0;
            _currentEnemy.GetComponentInChildren<ShieldHandler>().ChangeColour(_colourIndex);
        }
    }
}

