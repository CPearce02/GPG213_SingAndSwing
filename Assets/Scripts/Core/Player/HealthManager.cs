using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;

public class HealthManager : MonoBehaviour
{
    public int health;
    public CharacterData playerStats;
    Vector2 respawnPosition;

    void Awake()
    {
        health = playerStats.Health;

        respawnPosition = transform.position;
    }

    //Public so it can be called in other things such as a restart from checkpoint pause menu button.
    public void Respawn() => transform.position = respawnPosition;
}
