using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Core.Player;

public class PlatformingController : MonoBehaviour
{
    float moveSpeed => playerStats.MoveSpeed;
    [Range(1, 2)] public float friction;
    PlayerInput playerInput;
    Rigidbody2D rb;
    public CharacterData playerStats;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        float direction = playerInput.actions["Move"].ReadValue<float>();
        bool isPressingMove = direction != 0;

        if(isPressingMove)
        {
            rb.AddForce(new Vector2(moveSpeed * direction * Time.deltaTime, 0));
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x / friction, rb.velocity.y);
        }
    }
}