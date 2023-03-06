using Core.Player;
using Enums;
using Events;
using GameSections.Platforming.ScriptableObjects;
using Interfaces;
using Structs;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSections.Platforming
{
    public class EnemyPlatforming : MonoBehaviour, IAttackable
    {
        public EnemyData enemyDataData;
        public int damage = 10;
        public bool canBeDestroyed = false;
        [SerializeField] ParticleEvent takeDamageParticle;

        private void OnCollisionEnter2D(Collision2D collision) => HandleCollision2D(collision);

        private void OnTriggerEnter2D(Collider2D collider) => HandleCollision2D(collider);

        private void HandleCollision2D(Collider2D collider)
        {
            var attackable = collider.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
            if (!attackable) return;

            //Stops enemies from attacking each other
            collider.TryGetComponent(out PlatformingController player);
            if(player) attackableComponent.TakeDamage(damage);
        }
    
        private void HandleCollision2D(Collision2D collision)
        {
            var attackable = collision.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
            if (!attackable) return;

            //Stops enemies from attacking each other
            collision.transform.TryGetComponent(out PlatformingController player);
            if (player) attackableComponent.TakeDamage(damage);
        }

        public void TakeDamage(int amount)
        {
            if (canBeDestroyed == false) return;
            GameEvents.onScreenShakeEvent?.Invoke(Strength.Low, .2f);
            takeDamageParticle.Invoke();
            Destroy(gameObject);
        }
    }
}