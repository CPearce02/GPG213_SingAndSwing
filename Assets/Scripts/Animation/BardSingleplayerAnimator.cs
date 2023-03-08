using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BardSingleplayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerInput playerInput;

    Rigidbody2D _rb;
    SpriteRenderer _bardSprite;
    Animator _animator;
    BardController _bardController;

    Vector2 _lastPos;

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Falling = Animator.StringToHash("Falling");

    private void OnEnable()
    {
        playerInput.actions["Jump"].performed += SetJump;
    }

    private void OnDisable()
    {
        playerInput.actions["Jump"].performed -= SetJump;
    }

    void Start()
    {
        _bardSprite = GetComponent<SpriteRenderer>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _bardController = GetComponentInParent<BardController>();
        playerTransform = GameObject.Find("Player").transform;
        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }

    void Update()
    {
        FlipSprite();
        SetFalling();
        SetRunning();

        _animator.SetBool(Grounded, _bardController.Grounded);
    }

    private void LateUpdate()
    {
        _lastPos = transform.position;
    }

    void FlipSprite()
    {
        float _distanceFromPlayer = playerTransform.position.x - transform.position.x;

        if (_distanceFromPlayer >= 0) _bardSprite.flipX = false;
        else _bardSprite.flipX = true;
    }

    void SetFalling()
    {
        if (transform.position.y < _lastPos.y - 0.1f) _animator.SetBool(Falling, true);
        else _animator.SetBool(Falling, false);
    }

    void SetRunning()
    {
        if (transform.position.x > _lastPos.x + 0.1f || transform.position.x < _lastPos.x - 0.1f)
        {
            if (!_bardController.Grounded) return;
            _animator.SetBool(Moving, true);
        } else
        {
            _animator.SetBool(Moving, false);
        }
    }

    void SetJump(InputAction.CallbackContext context)
    {
        if (_bardController.Grounded && context.performed) _animator.SetTrigger(Jump);
    }
}
