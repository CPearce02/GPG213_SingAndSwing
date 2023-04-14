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
        [field: SerializeField] public Transform ChargeTransform { get; private set; }
        [Header("Boss State")]
        [SerializeField][ReadOnly] float stunCoolDownTime;
        [field: SerializeField] public bool HasBeenActivated { get; set; }
        [field: SerializeField] public bool CanBeStunned { get; set; }
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
            stunCoolDownTime = 0;
        }

        private void OnEnable()
        {
            if (enemy == null) enemy = GetComponent<Enemy>();
            // enemy.BossTakeDamage += ForceInterruptState;
            enemy.BossDeath += ForceDeathState;
        }

        private void OnDisable()
        {
            // enemy.BossTakeDamage -= ForceInterruptState;
            enemy.BossDeath -= ForceDeathState;
        }

        public override void Update()
        {
            base.Update();

            if (!HasBeenActivated) return;

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
                stunCoolDownTime = enemyData.stunCoolDown;
            }
        }

        void ForceDeathState()
        {
            if (CurrentState is BossDeathState) return;
            ChangeState(new BossDeathState());
        }


        public void SetHasBeenActivated() => HasBeenActivated = true;

        private void OnDrawGizmos()
        {
            var tr = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(tr, enemyData.triggerRange);
            //Charge Attack
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ChargeTransform.position, enemyData.chargeAttackSize);
        }

    }
}
