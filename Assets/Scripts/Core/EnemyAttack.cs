using Enemies;
using Enemies.ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class EnemyAttack : Attack
    {
        [SerializeField] private EnemyData enemyData;

        private void Start()
        {
            if(GetComponentInParent<Enemy>())
            {
                enemyData = GetComponentInParent<Enemy>().enemyData;
                if (enemyData)
                {
                    damageAmount = enemyData.damageAmount;
                    collider.size = new Vector2(enemyData.attackRange, enemyData.attackRange);
                }
            }
        }
    }
}