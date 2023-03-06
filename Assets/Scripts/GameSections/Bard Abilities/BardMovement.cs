using UnityEngine;
using Structs;

public class BardMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;
    public float followRange;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > followRange)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
