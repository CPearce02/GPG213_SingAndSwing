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

    public Transform groundCheckTransform;
    public LayerMask ignoreLayers;
    public Vector2 groundCheckSize;

    //holdingJump is used while the player is jumping, jumped is when the player has finished their jump.
    bool holdingJump = false, jumped = false;

    bool grounded = false, findGround = false;
    public bool Grounded { get => grounded; private set => grounded = value; }
    public bool FindGround { get => findGround; private set => findGround = value; }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        OnMove();
        OnJump();

        FindingGround();
    }

    void OnMove()
    {
        float direction = playerInput.actions["Move"].ReadValue<float>();
        bool isPressingMove = direction != 0;

        CapSpeed();

        if(isPressingMove)
        {
            //If the player is moving in the opposite direction and presses a different key and is on the ground, reset their x velocity.
            if (Grounded)
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
        var jump = playerInput.actions["Jump"];
        float pressing = jump.ReadValue<float>();
        bool isPressingJump = pressing != 0;

        if (Grounded) CalculateJumpHeight();

        if (isPressingJump && jumped == false)
        {
            //If the player wasn't already jumping, and they are on the ground, let them call the jump function.
            if (Grounded && !holdingJump) holdingJump = true;

            if (holdingJump) Jump();

            if (transform.position.y > relativeJumpHeight)
            {
                holdingJump = false; //Stop the player from continuing their jump if they finish their jump.
                jumped = true; //Prevent bunnyhopping.
            }
        }
        
        if (!isPressingJump)
        {
            //Stop the player from continuing their jump if they let go of the jump key.
            holdingJump = false;

            //Prevent bunnyhopping
            if (Grounded) jumped = false;
            else jumped = true;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
    }

    //Public void so it can be called in other scripts such as a jump pad.
    public void AddJump(float jumpHeight = 0)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }

    void CheckGround() => Grounded = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 0.1f, ~ignoreLayers);
    void FindingGround() => findGround = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 1f, ~ignoreLayers);

    void CalculateJumpHeight() => relativeJumpHeight = jumpHeight + transform.position.y;

    //Check ground only when the player is touching something.
    private void OnCollisionStay2D(Collision2D collision) => CheckGround();

    private void OnCollisionExit2D(Collision2D collision) => Grounded = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheckTransform.position, groundCheckSize);
    }
}