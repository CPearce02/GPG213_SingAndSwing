using UnityEngine;

namespace GameSections.Bard_Abilities
{
    public class BardMovement : MonoBehaviour
    {
        public Transform playerTransform; // Reference to the player's transform
        public float followSpeed;  // Speed at which the object follows the player
        public float stoppingDistance;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (playerTransform != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;

                if (Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance)
                {
                    rb.velocity = direction * followSpeed;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }
}
