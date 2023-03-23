using Enemies;
using Enemies.ScriptableObjects;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        public int damageAmount = 20;

        private void Start()
        {
            if(GetComponentInParent<Enemy>())
            {
                enemyData = GetComponentInParent<Enemy>().enemyData;
                if (enemyData) damageAmount = enemyData.damageAmount;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider) => HandleCollision2D(collider);

        private void HandleCollision2D(Collider2D collider)
        {
            var attackable = collider.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
            if (!attackable) return;
            attackableComponent.TakeDamage(damageAmount);
        }
    }
}
