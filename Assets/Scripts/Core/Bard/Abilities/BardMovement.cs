using UnityEngine;
using Structs;
using Events;
using Core.Player;
using System;

public class BardMovement : MonoBehaviour
{
    PlatformingController knightController;
    public Transform followObj;
    public float speed = 10f;
    public float followRange = 0.5f;
    public float relocateSpeed = 15f, relocateRange = 3f;
    private Rigidbody2D _rb;
    float _distanceX, _distance;

    private void Start()
    {
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
        _distance = Vector2.Distance(followObj.position, transform.position);
        _distanceX = followObj.position.x - transform.position.x;

        CheckRelocate();
    }

    private void FixedUpdate()
    {
        MoveBard();
    }

    void MoveBard()
    {
        if (_distanceX > followRange || _distanceX < -followRange)
            _rb.AddForce(new Vector2(_distanceX * speed * Time.fixedDeltaTime, 0));
        else if (knightController.Grounded) _rb.velocity = new Vector2(_rb.velocity.x / 1.5f, _rb.velocity.y);
    }

    public void BardJump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(transform.up * knightController.JumpSpeed, ForceMode2D.Impulse);
    }

    void CheckRelocate()
    {
        if (_distance > relocateRange) RelocateBard();
    }

    void RelocateBard()
    {
        transform.position = followObj.position;
        _rb.velocity = Vector2.zero;
    }

    private void SetPlayer(PlatformingController player)
    {
        knightController = player;

        knightController.JumpEvent += BardJump;
    }
}
