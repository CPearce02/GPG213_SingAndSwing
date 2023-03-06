using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bullet;
    public Transform directionTransform;
    [HideInInspector] public Transform player;
    public PhysicsMaterial2D bounceMaterial;

    [Header("Properties")]
    public bool aimAtPlayer = false;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;
    public float homingTime = 1f;

    bool countingDownShoot = false;

    public enum ShootState
    {
        ConstantDirection,
        Homing,
        Gravity,
        Bouncing
    }

    public ShootState shootState;

    void Update()
    {
        if(aimAtPlayer) DirectionToPlayer();

        if (!aimAtPlayer)
        {
            if (!countingDownShoot) StartCoroutine(DelayShoot());
        } else
        {
            if (!countingDownShoot && player != null) StartCoroutine(DelayShoot());
        }
    }

    void Shoot()
    {
        GameObject clonedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = clonedBullet.GetComponent<Rigidbody2D>();
        ProjectileController bManager = clonedBullet.AddComponent<ProjectileController>();
        Collider2D coll = clonedBullet.GetComponent<Collider2D>();

        rb.isKinematic = true;
        coll.isTrigger = true;

        if (player != null) bManager.player = player;
        if(shootState == ShootState.Homing) bManager.homingTime = homingTime;
        bManager.bulletSpeed = bulletSpeed;
        bManager.directionTransform = directionTransform;
        
        Physics2D.IgnoreCollision(coll, GetComponent<Collider2D>());

        if (shootState == ShootState.Gravity || shootState == ShootState.Bouncing)
        {
            coll.isTrigger = false;
            rb.isKinematic = false;
        }

        if(shootState == ShootState.Bouncing) rb.sharedMaterial = bounceMaterial;
    }

    void DirectionToPlayer()
    {
        if(player != null) directionTransform.position = player.transform.position;
    }

    IEnumerator DelayShoot()
    {
        countingDownShoot = true;
        yield return new WaitForSeconds(fireRate);
        countingDownShoot = false;
        Shoot();
    }
}
