using Interfaces;
using UnityEngine;

namespace Core.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public int damageAmount = 20;
        
        private void OnTriggerEnter2D(Collider2D collider) => HandleCollision2D(collider);

        private void HandleCollision2D(Collider2D collider)
        {
            var attackable = collider.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
            if (!attackable) return;
            attackableComponent.TakeDamage(damageAmount);
        }
    }
}
