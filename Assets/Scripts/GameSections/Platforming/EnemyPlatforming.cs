using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlatforming : MonoBehaviour
{
    HealthManager healthManager;
    public int damage = 10;

    public static void Damage(HealthManager healthManager, int damageAmount = 1) => healthManager.health -= damageAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player") return;

        healthManager = collision.transform.GetComponent<HealthManager>();

        Damage(healthManager, damage);
        healthManager.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;

        healthManager = collision.transform.GetComponent<HealthManager>();

        Damage(healthManager, damage);
        healthManager.Respawn();
    }
}
