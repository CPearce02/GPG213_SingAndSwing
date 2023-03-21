using Core.ScriptableObjects;
using UnityEngine;

namespace Enemies.ScriptableObjects
{
    [CreateAssetMenu (fileName = "New Enemy", menuName = "Enemy")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public float moveSpeed;
        public float attackRange;
        public float retreatTime;
        public Sprite enemySprite;
        public Sprite enemyAttackSprite;

        public int damageAmount;

        public int healthAmount;

        [field: SerializeField] public DamageType DamageType { get; private set; }

        [field: SerializeField] public Combo Combo { get; private set; }

    }
}
