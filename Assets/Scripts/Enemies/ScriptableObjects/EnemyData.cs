using Core.ScriptableObjects;
using Enums;
using UnityEngine;

namespace Enemies.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class EnemyData : ScriptableObject
    {
        [Header("General")]
        public EnemyType type;
        public string enemyName;
        public Sprite enemySprite;
        [field: Header("Combo")]
        [field: SerializeField] public Combo Combo { get; private set; }
        [Header("Movement")]
        public float moveSpeed;
        public float retreatTime;
        public float triggerRange;
        [Header("Attacking")]
        public float attackRange;
        public int damageAmount;
        public float attackCooldown;
        [Header("Health")]
        public int healthAmount;
        [Header("Logic")]
        public float decisionTime;
        [Header("Stun")]
        public float stunTime;
        public float stunCoolDown;
        [Header("Interrupt")]
        public float interruptTime;
        public float interruptCoolDown;
        [Header("Charge")]
        public int chargeDamage;
        public float chargeSpeedMultiplier;
        public float chargeAttackSize;
        [Header("Disappear")]
        public float disappearTime;
    }
}
