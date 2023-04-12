using Enemies.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BossUIHandler : MonoBehaviour
    {
        [SerializeField] EnemyData enemy;
        [SerializeField] TMP_Text bossName;

        public void SetEnemy(EnemyData enemyData)
        {
            this.enemy = enemyData;
            bossName.text = this.enemy.enemyName;
        }
    }
}
