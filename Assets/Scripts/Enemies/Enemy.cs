using System;
using Core.Player;
using Enemies.ScriptableObjects;
using Enums;
using Events;
using Interfaces;
using Structs;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IAttackable
    {
        public EnemyData enemyData;
        public int damage = 10;
        [SerializeField] private bool canBeDestroyed;
        [SerializeField] bool doesDamageOnCollision = true;
        [SerializeField] ParticleEvent takeDamageParticle;
        public event Action<bool> Destroyable;
        public event Action BossTakeDamage;
        public event Action BossDeath;

        public bool CanBeDestroyed
        {
            get => canBeDestroyed;
            set
            {
                canBeDestroyed = value; 
                Destroyable?.Invoke(CanBeDestroyed);   
            }
        }

        private int maxEnemyHealth;
        private int enemyhealth;
        public int EnemyHealth
        {
            get => enemyhealth;
            private set
            {
                enemyhealth = Mathf.Clamp(value, 0, maxEnemyHealth);
                var normalisedHealth = EnemyHealth / (float)maxEnemyHealth;
                //Send Boss Events
                if(TryGetComponent<BossEnemyStateMachine>(out BossEnemyStateMachine bossEnemyStateMachine))
                {
                    GameEvents.onBossHealthUIChangeEvent?.Invoke(normalisedHealth);
                    BossTakeDamage?.Invoke();
                }
                if (enemyhealth == 0)
                {
                    if (bossEnemyStateMachine)
                    {
                        GameEvents.onBossHealthUIChangeEvent?.Invoke(normalisedHealth);
                        BossDeath?.Invoke();
                    }
                    else
                    {
                        Destroy(gameObject);
                        Debug.Log("Enemy died");
                    }
                }
            }
        }
        
        private void Start()
        {
            if(enemyData != null)
            {
                damage = enemyData.damageAmount; 
                enemyhealth = enemyData.healthAmount;
                maxEnemyHealth = enemyhealth;
            }
            Destroyable?.Invoke(CanBeDestroyed);
        }

        // private void OnGUI()
        // {
        //     if (GUI.Button(new Rect(10, 10, 100, 30), "Destroy"))
        //     {
        //         if (CanBeDestroyed)
        //             SetCanBeDestroyed(false);
        //         else 
        //             SetCanBeDestroyed(true);
        //     }
        // }

        private void OnCollisionEnter2D(Collision2D collision) => HandleCollision2D(collision);
        
        private void HandleCollision2D(Collision2D collision)
        { 
            if(!doesDamageOnCollision) return;
            
            var attackable = collision.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
            if (!attackable) return;

            //Stops enemies from attacking each other
            collision.transform.TryGetComponent(out PlatformingController player);
            if(player) attackableComponent.TakeDamage(damage); Debug.Log($"{player} took {damage}");
        }

        public void TakeDamage(int amount)
        {
            if (!CanBeDestroyed) return;
            EnemyHealth -= amount;
            GameEvents.onScreenShakeEvent?.Invoke(Strength.Low, .2f);
            GameEvents.onMultiplierIncreaseEvent?.Invoke();
            takeDamageParticle.Invoke();
        }
        
        public void SetCanBeDestroyed(bool value) => CanBeDestroyed = value;

    }
}