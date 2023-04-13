using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Core.ScriptableObjects;
using Events;
using UnityEngine.UI;

namespace Core.Bard
{
    public class ComboManager : MonoBehaviour
    {
        public List<Enemy> enemies = new List<Enemy>();
        private Enemy _selectedEnemy;
        private int _enemyIndex;

        // Start is called before the first frame update
        void Start()
        {
            //Clear list before every level
            enemies.Clear();
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //Choose Enemy if there are multiple
            if (enemies.Count < 1) return;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_enemyIndex < enemies.Count - 1)
                {
                    _enemyIndex += 1;
                }
                else
                {
                    _enemyIndex = 0;
                }
                _selectedEnemy = enemies[_enemyIndex];
                UpdateShader();
                //Send Combo to UI
                GameEvents.onNewCombo?.Invoke(enemies[_enemyIndex].enemyData.Combo);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                if (enemyComponent.enemyData != null && !enemies.Contains(enemyComponent))
                {
                    enemies.Add(enemyComponent);
                    _selectedEnemy = enemyComponent;
                    UpdateShader();
                    _enemyIndex += 1;
                    GameEvents.onNewCombo?.Invoke(enemyComponent.enemyData.Combo);
                }
            }
        }

        private void UpdateShader()
        {
            //Update Colour
            foreach (Enemy enemy in enemies)
            {
                if (enemy != _selectedEnemy)
                {
                    // this throws null exception
                    enemy.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    enemy.GetComponentInChildren<SpriteRenderer>().color = Color.magenta;
                }
            }
        }
    }
}

