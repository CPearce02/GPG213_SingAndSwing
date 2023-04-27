using Interfaces;
using UnityEngine;

namespace Core
{
    public class Attack : MonoBehaviour
    {
        public int damageAmount = 20;
        [SerializeField] protected new BoxCollider2D collider;
        AudioSource _audioSrc;

        private void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
            _audioSrc = GetComponent<AudioSource>();
        }
        
        void OnTriggerEnter2D(Collider2D collider) => HandleCollision2D(collider);

        void HandleCollision2D(Collider2D collider)
        {
            var attackable = collider.TryGetComponent<IAttackable>(out var attackableComponent);
            if (!attackable) return;
            attackableComponent.TakeDamage(damageAmount);
            _audioSrc.Play();
        }
    }
}
