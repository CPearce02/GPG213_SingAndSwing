using UnityEngine;
using Structs;
using Events;
using Core.Player;
using System;

public class BardMovement : MonoBehaviour
{
    PlatformingController _knightController;
    public Transform followObj;
    public float speed = 10f;
    [Tooltip("1 for no friction, 2 for LOTS of friction.")][Range(1, 2)] [SerializeField] float friction = 1.5f;
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
        _knightController.JumpEvent -= BardJump;
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
        else _rb.velocity = new Vector2(_rb.velocity.x / friction, _rb.velocity.y);

        //For the original rubber bandy movement, do "else if (_knightController.Grounded)", instead of the else seen here.
    }

    public void BardJump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(transform.up * _knightController.JumpSpeed, ForceMode2D.Impulse);
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
        _knightController = player;

        _knightController.JumpEvent += BardJump;
    }
}
