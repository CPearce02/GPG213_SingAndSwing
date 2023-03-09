using UnityEngine;
using Structs;
using Events;
using Core.Player;
using System;

public class BardMovement : MonoBehaviour
{
    PlatformingController knightController;
    private Transform _playerTransform;
    private float _speed = 10f;
    public float followRange;
    private Rigidbody2D _rb;
    float _distance;
    Vector2 direction, newPosition;

    private void Start()
    {
        _playerTransform = GameObject.Find("Follow").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameEvents.onSendPlayerEvent += SetPlayer;
    }

    private void OnDisable()
    {
        GameEvents.onSendPlayerEvent -= SetPlayer;
        knightController.JumpEvent -= BardJump;
    }

    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _playerTransform.position);

        direction = (_playerTransform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (_distance > followRange)
        {
            newPosition = _rb.position + direction * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        } else
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }

    public void BardJump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(transform.up * knightController.JumpSpeed, ForceMode2D.Impulse);
    }

    private void SetPlayer(PlatformingController player)
    {
        _playerTransform = player.FollowTransform;
        knightController = player;

        knightController.JumpEvent += BardJump;
    }
}
