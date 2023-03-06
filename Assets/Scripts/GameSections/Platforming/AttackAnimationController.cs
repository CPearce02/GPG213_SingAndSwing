using UnityEngine;

namespace GameSections.Platforming
{
    public class AttackAnimationController : MonoBehaviour
    {
        Animator _animator;
        public Collider2D coll; 

        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("knight_attack")) DisableHitbox();
        }

        void EnableHitbox() => coll.enabled = true;
        void DisableHitbox() => coll.enabled = false;
    }
}
