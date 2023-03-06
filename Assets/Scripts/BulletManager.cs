using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Core.Player;
using GameSections.Platforming;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [Description("How long the bullet is alive + the homing time")]
    public float bulletAliveTime = 5f;

    [HideInInspector] public float homingTime = 0;
    float originalHomingTime = 0;
    [HideInInspector] public Transform player, directionTransform;
    [HideInInspector] public float bulletSpeed;
    Rigidbody2D rb;

    void Start()
    {
        originalHomingTime = homingTime;
        rb = GetComponent<Rigidbody2D>();
        bulletAliveTime += homingTime;

        if (!player || homingTime == 0)
        {
            rb.velocity = bulletSpeed * (directionTransform.position - transform.position).normalized;
        }
    }

    void Update()
    {
        if (originalHomingTime > 0)
        {
            originalHomingTime -= Time.deltaTime;

            rb.velocity = bulletSpeed * (player.position - transform.position).normalized;
        }

        if (bulletAliveTime > 0) bulletAliveTime -= Time.deltaTime;
        else DestroyBullet();
    }

    //You can change this function to give the bullet cool effects or whatever
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out PlatformingController player);
        if (player) DestroyBullet();
    }
}
