using UnityEngine;

public class AttackManager : MonoBehaviour
{
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
