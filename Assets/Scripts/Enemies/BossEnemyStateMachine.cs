using System.Collections;
using Enemies.BossStates;
using Events;
using UnityEngine;
using Core.ScriptableObjects;

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

        public Combo[] comboList;

        [SerializeField] Collider2D[] collisionsToTurnOffOnDeath;

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
            enemy.BossDeath += RoomTransition;
        }

        private void OnDisable()
        {
            // enemy.BossTakeDamage -= ForceInterruptState;
            enemy.BossDeath -= ForceDeathState;
            enemy.BossDeath -= RoomTransition;
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

        void RoomTransition()
        {
            
            StartCoroutine(RoomT());
        }

        IEnumerator RoomT()
        {
            yield return new WaitForSeconds(1f);
            GameEvents.onScreenShakeEvent?.Invoke(Enums.Strength.High, 2f);

            foreach (Collider2D coll in collisionsToTurnOffOnDeath)
            {
                coll.usedByComposite = false;
            }

            yield return new WaitForSeconds(2f);

            foreach(Collider2D coll in collisionsToTurnOffOnDeath)
            {
                coll.enabled = false;
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
