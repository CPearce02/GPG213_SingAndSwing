using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bullet;

    public Transform directionTransform;

    public enum ShootState
    {
        fixedDirection,
        followPlayer,
        both
    }

    public ShootState shootState;

    private void Start()
    {
        GameObject clonedBullet = Instantiate(bullet, directionTransform.position, Quaternion.identity);

        clonedBullet.GetComponent<Rigidbody2D>().isKinematic = true;
        clonedBullet.GetComponent<Collider2D>().isTrigger = true;
    }

    void Update()
    {
        
    }

    void Shoot()
    {

    }

    void ShootFollow()
    {

    }
}
