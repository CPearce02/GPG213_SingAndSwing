using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HalfPlatform : MonoBehaviour
{
    Rigidbody2D playerRb;
    PlatformingController platformingController;

    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        platformingController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformingController>();
    }

    void Update()
    {
        if (playerRb.velocity.y > 0.01f) DisableCollision();
        if (playerRb.velocity.y <= 0)
        {
            if (platformingController.FindGround == false)
            {
                DisableCollision();
                return;
            }

            EnableCollision();
        }
    }

    void EnableCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    void DisableCollision()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
    }
}
