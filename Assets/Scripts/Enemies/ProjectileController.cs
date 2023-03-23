using System.ComponentModel;
using Core.Player;
using Structs;
using UnityEngine;
using Interfaces;

namespace Enemies
{
    public class ProjectileController : MonoBehaviour
    {
        [Description("How long the bullet is alive + the homing time")]
        public float bulletAliveTime = 5f;

        [ReadOnly] public float homingTime = 0;
        float _originalHomingTime = 0;
        [field: ReadOnly, SerializeField] public Transform Player { get; set; }
        [field: ReadOnly, SerializeField] public Transform DirectionTransform { get; set; }
        [field: ReadOnly, SerializeField] public float BulletSpeed { get; set; }
        [SerializeField] private float rotationOffset = 180f;
        public Rigidbody2D Rb { get; private set; }
        public Collider2D Collider { get; private set; }
        public ParticleEvent onBulletDestroyParticle;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();
        }

        void Start()
        {
            _originalHomingTime = homingTime;
           
            bulletAliveTime += homingTime;

            if (!Player || homingTime == 0)
            {
                Rb.velocity = BulletSpeed * (DirectionTransform.position - transform.position).normalized;
            }
        }

        void Update()
        {
            if (_originalHomingTime > 0)
            {
                _originalHomingTime -= Time.deltaTime;

                if (Player != null) Rb.velocity = BulletSpeed * (Player.position - transform.position).normalized;
            }

            if (bulletAliveTime > 0) bulletAliveTime -= Time.deltaTime;
            else DestroyBullet();
            
            RotateInDirectionOfVelocity();
        }

        private void RotateInDirectionOfVelocity()
        {
            float angle = Mathf.Atan2(Rb.velocity.y, Rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
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
            if (player) {
                 var attackable = collision.gameObject.TryGetComponent<IAttackable>(out var attackableComponent);
                if (!attackable) return;

            //Stops enemies from attacking each other
                attackableComponent.TakeDamage(15);
                DestroyBullet(); }
        }
    }
}
