using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Core.ScriptableObjects;
using Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Core.Bard
{
    public class ComboManager : MonoBehaviour
    {
        private Enemy _currentEnemy;
        public List<Enemy> _enemies = new List<Enemy>();
        private int _comboListIndex;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void OnEnable()
        {
            GameEvents.onComboFinish += ComboFinished;
        }        

        private void OnDisable()
        {
            GameEvents.onComboFinish -= ComboFinished;
        }

        // Update is called once per frame
        void Update()
        {
            //Choose Enemy if there are multiple
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_comboListIndex < _enemies.Count - 1)
                {
                    _comboListIndex += 1;
                }
                else
                {
                    _comboListIndex = 0;
                }
                _currentEnemy = _enemies[_comboListIndex];
                SetCombo(_enemies[_comboListIndex].enemyData.Combo);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                //Check if the enemy has a combo, isn't already within the enemies list and if the enemy has a shield. 
                if (enemyComponent.enemyData.Combo != null && !_enemies.Contains(enemyComponent) && !enemyComponent.CanBeDestroyed)
                {
                    _enemies.Add(enemyComponent);
                    SetCombo(enemyComponent.enemyData.Combo);
                    _currentEnemy = enemyComponent;
                }
            }
        }

        private void SetCombo(Combo _combo)
        {
            UpdateShader();
            GameEvents.onNewCombo?.Invoke(_combo);
        }

        private void UpdateShader()
        {
            //Update Colour
            foreach (Enemy enemy in _enemies)
            {
                if (enemy == _currentEnemy)
                {
                    enemy.GetComponentInChildren<SpriteRenderer>().color = Color.magenta;
                }
                else
                {
                    enemy.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
            }
        }

        private void ComboFinished(bool completed)
        {
            if(completed)
            {
                _currentEnemy.SetCanBeDestroyed(true);
                _enemies.Remove(_currentEnemy);
            }
            else
            {
                //Play fail audio
            }
        }

    }
}

