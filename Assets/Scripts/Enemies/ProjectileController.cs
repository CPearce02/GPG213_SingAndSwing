using System.ComponentModel;
using Core.Player;
using Structs;
using UnityEngine;

namespace Enemies
{
    public class ProjectileController : MonoBehaviour
    {
        [Description("How long the bullet is alive + the homing time")]
        public float bulletAliveTime = 5f;

        [ReadOnly] public float homingTime = 0;
        float _originalHomingTime = 0;
        [ReadOnly] public Transform player, directionTransform;
        [ReadOnly] public float bulletSpeed;
        Rigidbody2D rb;
        
        public ParticleEvent onBulletDestroyParticle;

        void Start()
        {
            _originalHomingTime = homingTime;
            rb = GetComponent<Rigidbody2D>();
            bulletAliveTime += homingTime;

            if (!player || homingTime == 0)
            {
                rb.velocity = bulletSpeed * (directionTransform.position - transform.position).normalized;
            }
        }

        void Update()
        {
            if (_originalHomingTime > 0)
            {
                _originalHomingTime -= Time.deltaTime;

                if (player != null) rb.velocity = bulletSpeed * (player.position - transform.position).normalized;
            }

            if (bulletAliveTime > 0) bulletAliveTime -= Time.deltaTime;
            else DestroyBullet();
        }

        //You can change this function to give the bullet cool effects or whatever
        public void DestroyBullet()
        {
            onBulletDestroyParticle.Invoke();
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.TryGetComponent(out PlatformingController player);
            if (player) DestroyBullet(); 
        }
    }
}
