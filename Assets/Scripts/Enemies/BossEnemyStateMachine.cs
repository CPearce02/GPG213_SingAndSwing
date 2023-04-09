using Enemies.BossStates;
using UnityEngine;

namespace Enemies
{
    public class BossEnemyStateMachine : EnemyStateMachine
    {
        public Transform target;
        public bool canBeStunned;
        public float decideAttackTime;
        [SerializeField] float stunCoolDownTime;
        float originalStunCoolDownTime;

        public override void Start()
        {
            ChangeState(new BossIdleState());
            originalStunCoolDownTime = stunCoolDownTime;
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
