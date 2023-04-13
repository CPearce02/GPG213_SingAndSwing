using Enemies.BossStates;
using Events;
using UnityEngine;

namespace Enemies
{
    public class BossEnemyStateMachine : EnemyStateMachine
    {
        
        [Header("Boss Settings")]
        [SerializeField] private Enemy enemy;
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Collider2D MainCollider { get; private set; }
        [Header("Boss State")]
        public float decideAttackTime;
        [SerializeField] float stunCoolDownTime;
        [field: SerializeField] public float DisappearTime { get; private set; }
        [field: SerializeField] public float InterruptTime { get; private set; }
        [field: SerializeField] public float ChargeSpeedMultiplier { get; private set; }
        [field: SerializeField] public bool HasBeenActivated { get; set; }
        [field: SerializeField] public bool CanBeStunned { get; set; }
        private float originalStunCoolDownTime;
        public Transform target;
        public Transform[] positions;

        private void Awake()
        {
            if (SpriteRenderer == null) SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (MainCollider == null) MainCollider = GetComponentInChildren<Collider2D>();
        }

        public override void Start()
        {
            ChangeState(new BossIdleState());
            originalStunCoolDownTime = stunCoolDownTime;
        }

        private void OnEnable()
        {
            if (enemy == null) enemy = GetComponent<Enemy>();
            enemy.BossTakeDamage += ForceInterruptState;
            enemy.BossDeath += ForceDeathState;
        }

        private void OnDisable()
        {
            enemy.BossTakeDamage -= ForceInterruptState;
            enemy.BossDeath -= ForceDeathState;
        }

        public override void Update()
        {
            base.Update();

            UpdateCanBeStunned();
        }

        private void UpdateCanBeStunned()
        {
            if (!CanBeStunned && stunCoolDownTime > 0)
            {
                stunCoolDownTime -= Time.deltaTime;
            }
            else
            {
                CanBeStunned = true;
                stunCoolDownTime = originalStunCoolDownTime;
            }
        }
        
        void ForceInterruptState()
        {
            ChangeState(new BossInterruptedState());
        }
        
        void ForceDeathState()
        {
            ChangeState(new BossDeathState());
        }
        

        public void SetHasBeenActivated() => HasBeenActivated = true;

        private void OnDrawGizmos()
        {
            var tr = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(tr, enemyData.triggerRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(tr, enemyData.attackRange);
        }
        
    }
}
