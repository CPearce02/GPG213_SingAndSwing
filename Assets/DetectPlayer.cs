using System.Collections;
using System.Collections.Generic;
using Core.Player;
using GameSections.Platforming;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public ShootingEnemy shootingEnemyScript;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.TryGetComponent(out PlatformingController player);
        if (player) shootingEnemyScript.player = player.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shootingEnemyScript.player = null;
    }
}
