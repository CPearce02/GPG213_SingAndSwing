using System;
using System.Collections;
using System.Collections.Generic;
using Core.Player;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class BardSingleplayerAnimator : MonoBehaviour
{
    [SerializeField][ReadOnly] private Transform playerTransform;
    [SerializeField][ReadOnly] private PlayerInput playerInput;

    Rigidbody2D _rb;
    SpriteRenderer _bardSprite;
    Animator _animator;
    BardController _bardController;

    Vector2 _lastPos;

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Falling = Animator.StringToHash("Falling");

    void Awake()
    {
        _bardSprite = GetComponent<SpriteRenderer>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _bardController = GetComponentInParent<BardController>();
    }

    private void Start() => GameEvents.onRequestPlayerEvent?.Invoke();

    private void OnEnable()
    {
        GameEvents.onSendPlayerEvent += SetPlayer;
    }

    private void OnDisable()
    {
        GameEvents.onSendPlayerEvent -= SetPlayer;
        if (playerInput != null)
            playerInput.actions["Jump"].performed -= SetJump;
    }

    private void SetPlayer(PlatformingController player)
    {
        playerInput = player.PlayerInput;
        playerTransform = player.transform;
        playerInput.actions["Jump"].performed += SetJump;
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
        }
        else
        {
            _animator.SetBool(Moving, false);
        }
    }

    void SetJump(InputAction.CallbackContext context)
    {
        if (_bardController.Grounded && context.performed) _animator.SetTrigger(Jump);
    }
}
