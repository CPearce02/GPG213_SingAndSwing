using Enemies.BossStates;
using Events;
using UnityEngine;

namespace Enemies
{
    public class BossEnemyStateMachine : EnemyStateMachine
    {
        private Enemy enemy;
        public Transform target;
        public bool canBeStunned;
        public float decideAttackTime;
        [SerializeField] float stunCoolDownTime;
        private float originalStunCoolDownTime;
        [field:SerializeField] public bool HasBeenActivated { get; private set; }
        public Transform[] positions;

        public override void Start()
        {
            ChangeState(new BossIdleState());
            originalStunCoolDownTime = stunCoolDownTime;
        }
        
        private void OnEnable()
        {
            if(enemy == null) enemy = GetComponent<Enemy>();
            enemy.BossTakeDamage += () => ChangeState(new BossInterruptedState());
            enemy.BossDeath += () => ChangeState(new BossDeathState());
        }

        private void OnDisable()
        {
            enemy.BossTakeDamage -= () => ChangeState(new BossInterruptedState());
            enemy.BossDeath -= () => ChangeState(new BossDeathState());
        }
        
        public override void Update()
        {
            base.Update();

            UpdateCanBeStunned();


        }
        
        private void UpdateCanBeStunned()
        {
            if (!canBeStunned && stunCoolDownTime > 0)
            {
                stunCoolDownTime -= Time.deltaTime;
            }
            else
            {
                canBeStunned = true;
                stunCoolDownTime = originalStunCoolDownTime;
            }
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
