using Core.ScriptableObjects;
using GameSections.Bard_Abilities.ScriptableObject;
using UnityEngine;

namespace GameSections.Platforming.ScriptableObjects
{
    [CreateAssetMenu (fileName = "New Enemy", menuName = "Enemy")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;

        public Sprite enemySprite;
        public Sprite enemyAttackSprite;

        public int damageAmount;

        public int healthAmount;

        [field: SerializeField] public DamageType DamageType { get; private set; }

        [field: SerializeField] public Combo Combo { get; private set; }

    }
}
