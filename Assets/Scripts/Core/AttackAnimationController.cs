using UnityEngine;

namespace Core
{
    public class AttackAnimationController : MonoBehaviour
    {
        Animator _animator;
        public Collider2D coll; 

        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        void EnableHitbox() => coll.enabled = true;
        void DisableHitbox() => coll.enabled = false;
    }
}
