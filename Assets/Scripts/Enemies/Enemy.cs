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

        public bool CanBeDestroyed
        {
            get => canBeDestroyed;
            set
            {
                canBeDestroyed = value; 
                Destroyable?.Invoke(CanBeDestroyed);   
            }
        }
        
        private void Start()
        {
            if(enemyData != null) damage = enemyData.damageAmount;
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
            GameEvents.onScreenShakeEvent?.Invoke(Strength.Low, .2f);
            GameEvents.onMultiplierIncreaseEvent?.Invoke();
            takeDamageParticle.Invoke();
            Destroy(gameObject);
        }
        
        public void SetCanBeDestroyed(bool value) => CanBeDestroyed = value;

    }
}