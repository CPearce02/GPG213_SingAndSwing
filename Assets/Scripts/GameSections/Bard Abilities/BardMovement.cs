using UnityEngine;
using Structs;
using Events;
using Core.Player;
using System;

public class BardMovement : MonoBehaviour
{
    //private BardController _bardController;
    private Transform _playerTransform;
    private float _speed = 10f;
    public float followRange;
    private Rigidbody2D rb;

    private void Start()
    {
        //_bardController = GetComponent<BardController>();
        _playerTransform = GameObject.Find("Follow").transform;
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnEnable()
    {
        GameEvents.onSendPlayerEvent += SetPlayer;
    }

    private void OnDisable()
    {
        GameEvents.onSendPlayerEvent += SetPlayer;
    }


    private void FixedUpdate()
    {
        //if (Vector2.Distance(transform.position, _playerTransform.position) > 5)
        //{
        //    transform.position = _playerTransform.position;
        //}
        if (Vector2.Distance(transform.position, _playerTransform.position) > followRange)
        {
            Vector2 direction = (_playerTransform.position - transform.position).normalized;
            if (direction.y >= 0) return;
            Vector2 newPosition = rb.position + direction * _speed * Time.fixedDeltaTime;
            //Vector2 smoothedPosition = Vector2.Lerp(rb.position, newPosition, 0.5f); // Smooth the movement using Lerp
            rb.MovePosition(newPosition);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void SetPlayer(PlatformingController player) => _playerTransform = player.FollowTransform;

}
