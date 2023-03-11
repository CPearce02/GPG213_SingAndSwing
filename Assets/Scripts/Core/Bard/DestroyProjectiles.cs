using Enemies;
using Structs;
using UnityEngine;

namespace Core.Bard
{
    public class DestroyProjectiles : MonoBehaviour
    {
        [SerializeField] ParticleEvent destroyProjectileParticle;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<ProjectileController>(out ProjectileController pc))
            {
                pc.DestroyBullet();
                destroyProjectileParticle.Invoke();
            }
        }
    }
}
