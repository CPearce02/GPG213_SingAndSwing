using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;

    public Sprite enemySprite;
    public Sprite enemyAttackSprite;

    public float damageAmount;

    public float healthAmount;

    public enum DamageType {Fire, Earth, Water, Lightning, Wind}
    public DamageType damageType;

    public enum DamageWeak { Fire, Earth, Water, Lightning, Wind }
    public DamageType damageWeak;
}
