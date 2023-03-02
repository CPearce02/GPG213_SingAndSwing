using UnityEngine;
using Structs;

public class FollowPlayer : MonoBehaviour
{
    private ParticleEvent particleEvent;
    public Transform player; // Reference to the player object
    public float xOffset; // The distance the 2D object should be from the player on the x-axis
    private Vector3 offset; // The offset between the player and the 2D object

    void Start()
    {
        offset = transform.position - player.position; // Calculate the offset between the player and the 2D object
    }

    void LateUpdate()
    {
        // Set the position of the 2D object to the player's position plus the offset
        transform.position = new Vector3(player.position.x + xOffset, player.position.y + offset.y, player.position.z + offset.z);
    }

    // Make the 2D object jump with the player
    void FixedUpdate()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();

        if (rb2D != null && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
    }
}
