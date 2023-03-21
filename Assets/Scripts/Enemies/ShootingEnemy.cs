using System.Collections;
using Enums;
using Interfaces;
using Structs;
using UnityEngine;

namespace Enemies
{
    public class ShootingEnemy : MonoBehaviour, ITarget
    {
        [Header("Prefabs")] 
        [SerializeField] private GameObject projectile;
        [SerializeField] Transform directionTransform;
        [HideInInspector] public Transform player;
        [SerializeField] PhysicsMaterial2D bounceMaterial;

        [Header("Properties")]
        [SerializeField] bool aimAtPlayer;
        [SerializeField] bool countingDownShoot;
        [SerializeField] bool disableUpdate = false;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float bulletSpeed = 10f;
        [SerializeField] private float homingTime = 1f;
        
        public ShootState shootState;
        
        [SerializeField] ParticleEvent onBulletDestroyParticle;
        private Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        void Update()
        {
            if(aimAtPlayer) DirectionToPlayer();

            if (!disableUpdate) Shooting();
        }

        private void Shooting()
        {
            if (aimAtPlayer)
            {
                if (!countingDownShoot && player != null) StartCoroutine(DelayShoot());
            }
            else
            {
                if (!countingDownShoot) StartCoroutine(DelayShoot());
            }
        }

        public void Shoot()
        {
            if(projectile == null) return;
            
            var clonedBullet = InstantiateProjectile();
            
            ConfigureProjectile(clonedBullet);
        }

        private void ConfigureProjectile(ProjectileController clonedBullet)
        {
            clonedBullet.onBulletDestroyParticle = onBulletDestroyParticle;
            clonedBullet.onBulletDestroyParticle.Transform = clonedBullet.transform;
            clonedBullet.Rb.isKinematic = true;
            clonedBullet.Collider.isTrigger = true;

            if (player != null) clonedBullet.Player = player;
            if (shootState == ShootState.Homing) clonedBullet.homingTime = homingTime;
            clonedBullet.BulletSpeed = bulletSpeed;
            clonedBullet.DirectionTransform = directionTransform;

            Physics2D.IgnoreCollision(clonedBullet.Collider, _collider2D);
            if (shootState == ShootState.Gravity || shootState == ShootState.Bouncing)
            {
                clonedBullet.Collider.isTrigger = false;
                clonedBullet.Rb.isKinematic = false;
            }

            if (shootState == ShootState.Bouncing) clonedBullet.Rb.sharedMaterial = bounceMaterial;
        }

        private ProjectileController InstantiateProjectile() =>
            Instantiate(projectile, transform.position, Quaternion.identity)
                .AddComponent<ProjectileController>();

        void DirectionToPlayer()
        {
            if(player == null) return;
            directionTransform.position = player.transform.position;
        }

        IEnumerator DelayShoot()
        {
            countingDownShoot = true;
            yield return new WaitForSeconds(fireRate);
            countingDownShoot = false;
            Shoot();
        }

        public void SetTarget(Transform target)
        {
            player = target;
        }

        public void RemoveTarget()
        {
            player = null;
        }
    }
}
