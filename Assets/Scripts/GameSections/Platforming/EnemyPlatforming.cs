using Interfaces;
using UnityEngine;
using Events;
using Enums;
using Structs;

public class EnemyPlatforming : MonoBehaviour, IAttackable
{
    public int damage = 10;
    public bool canBeDestroyed = false;
    [SerializeField] ParticleEvent takeDamageParticle;

    private void OnCollisionEnter2D(Collision2D collision) => HandleCollision2D(collision);

    private void OnTriggerEnter2D(Collider2D collider) => HandleCollision2D(collider);

    private void HandleCollision2D(Collider2D collider)
    {
        var attackable = collider.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
        if (!attackable) return;
        attackableComponent.TakeDamage(damage);
    }
    
    private void HandleCollision2D(Collision2D collision)
    {
        var attackable = collision.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
        if (!attackable) return;
        attackableComponent.TakeDamage(damage);
    }

    public void TakeDamage(int amount)
    {
        if (canBeDestroyed == false) return;
        GameEvents.onScreenShakeEvent?.Invoke(Strength.Low, .2f);
        takeDamageParticle.Invoke();
        Destroy(gameObject);
    }
}