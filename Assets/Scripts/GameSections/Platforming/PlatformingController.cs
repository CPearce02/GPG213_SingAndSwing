using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Core.Player;

public class PlatformingController : MonoBehaviour
{
    float moveSpeed => playerStats.MoveSpeed;
    float jumpSpeed => playerStats.JumpSpeed;
    float jumpHeight => playerStats.JumpHeight;
    public float relativeJumpHeight = 0;

    [Range(1, 2)] public float friction;
    public float speedLimit = 1000f;

    PlayerInput playerInput;
    Rigidbody2D rb;
    public CharacterData playerStats;

    bool grounded = false;

    public Transform groundCheckTransform;
    public LayerMask ignoreLayers;
    public Vector2 groundCheckSize;

    bool holdingJump = false, jumped = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        OnMove();

        if (grounded) CalculateJumpHeight();

        OnJump();
    }

    void OnMove()
    {
        float direction = playerInput.actions["Move"].ReadValue<float>();
        bool isPressingMove = direction != 0;

        CapSpeed();

        if(isPressingMove)
        {
            //If the player is moving in the opposite direction and presses a different key and is on the ground, reset their x velocity.
            if (grounded)
            {
                if (direction > 0 && rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
                if (direction < 0 && rb.velocity.x > 0) rb.velocity = new Vector2(0, rb.velocity.y);
            }

            rb.AddForce(new Vector2(moveSpeed * direction * Time.deltaTime, 0));
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x / friction, rb.velocity.y);
        }
    }

    void CapSpeed()
    {
        if (rb.velocity.x > speedLimit) rb.velocity = new Vector2(speedLimit, rb.velocity.y);
        if (rb.velocity.x < -speedLimit) rb.velocity = new Vector2(-speedLimit, rb.velocity.y);
    }

    void OnJump()
    {
        float pressing = playerInput.actions["Jump"].ReadValue<float>();
        bool isPressingJump = pressing != 0;

        if (isPressingJump && jumped == false)
        {
            if (grounded && !holdingJump) holdingJump = true;

            if (holdingJump) Jump();

            if (transform.position.y > relativeJumpHeight)
            {
                holdingJump = false;
                jumped = true;
            }
        }

        if (!isPressingJump)
        {
            holdingJump = false;

            if (grounded) jumped = false;
            else jumped = true;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
    }

    //Public void so it can be called in other scripts such as a jump pad.
    public void AddJump(float additionalHeight = 0)
    {
        float calculatedJumpHeight = jumpSpeed + additionalHeight;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * calculatedJumpHeight, ForceMode2D.Impulse);
    }

    void CheckGround() => grounded = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 0.1f, ~ignoreLayers);

    void CalculateJumpHeight() => relativeJumpHeight = jumpHeight + transform.position.y;

    //Check ground only when the player is touching something.
    private void OnCollisionStay2D(Collision2D collision) => CheckGround();

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheckTransform.position, groundCheckSize);
    }
}