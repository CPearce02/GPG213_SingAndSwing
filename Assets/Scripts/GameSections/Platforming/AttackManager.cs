using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interfaces;

public class AttackManager : MonoBehaviour
{
    public int damageAmount;

    Animator animator;
    public Collider2D coll; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("knight_attack")) DisableHitbox();
    }

    void EnableHitbox() => coll.enabled = true;
    void DisableHitbox() => coll.enabled = false;
}
