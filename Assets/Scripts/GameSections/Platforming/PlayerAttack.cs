using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount = 20;

    private void OnCollisionEnter2D(Collision2D collision) => HandleCollision2D(collision);

    private void OnTriggerEnter2D(Collider2D collider) => HandleCollision2D(collider);

    private void HandleCollision2D(Collider2D collider)
    {
        var attackable = collider.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
        if (!attackable) return;
        attackableComponent.TakeDamage(damageAmount);
    }

    private void HandleCollision2D(Collision2D collision)
    {
        var attackable = collision.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
        if (!attackable) return;
        attackableComponent.TakeDamage(damageAmount);
    }
}
