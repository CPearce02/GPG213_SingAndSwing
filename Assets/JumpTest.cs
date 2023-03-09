using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    public float force = 15f;
    public int jumpTimes = 1;
    int j;
    bool jumped = false;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            j = jumpTimes;
            jumped = true;
        }

        if (Input.GetKeyDown(KeyCode.F)) rb.velocity = Vector2.zero;

        if (jumped) Jump();
    }

    void Jump()
    {
        float f = force / jumpTimes;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * f, ForceMode2D.Impulse);

        j--;

        if (j <= 0) jumped = false;
    }
}
