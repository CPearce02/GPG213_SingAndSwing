using Interfaces;
using UnityEngine;

public class EnemyPlatforming : MonoBehaviour
{
    public int damage = 10;
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
}
