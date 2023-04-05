using UnityEngine;

namespace Core
{
    public class AttackAnimationController : MonoBehaviour
    {
        public Collider2D coll; 
        
        void EnableHitbox() => coll.enabled = true;
        void DisableHitbox() => coll.enabled = false;
    }
}
