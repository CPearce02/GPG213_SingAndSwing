using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator animator;
    public PlayerInput playerInput;
    public Rigidbody2D rb;
    public PlatformingController platformingController;
    bool isFalling;

    private static readonly int Falling = Animator.StringToHash("IsFalling");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int XVelocity = Animator.StringToHash("XVelocity");
    private static readonly int Jump = Animator.StringToHash("Jump");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInput.actions["Jump"].performed += SetJump;
    }

    void Update()
    {
        SetFalling();
        SetGrounded();
    }

    void SetFalling()
    {
        if (platformingController.Grounded) return;

        isFalling = rb.velocity.y < 0;
        animator.SetBool(Falling, isFalling);
    }

    void SetGrounded() => animator.SetBool(Grounded, platformingController.Grounded);

    void SetJump(InputAction.CallbackContext context)
    {
        if(platformingController.Grounded) animator.SetTrigger(Jump);
    }
}
