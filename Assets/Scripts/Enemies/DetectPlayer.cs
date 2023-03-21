using Core.Player;
using UnityEngine;

namespace Enemies
{
    public class DetectPlayer : MonoBehaviour
    {
        public ShootingEnemy shootingEnemyScript;

        private void OnTriggerStay2D(Collider2D collision)
        {
            collision.TryGetComponent(out PlatformingController player);
            if (player) shootingEnemyScript.SetTarget(player.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            collision.TryGetComponent(out PlatformingController player);
            if (player) shootingEnemyScript.RemoveTarget();

        }
    }
}
