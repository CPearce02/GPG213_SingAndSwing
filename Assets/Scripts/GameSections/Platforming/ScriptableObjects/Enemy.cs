using UnityEngine;

[CreateAssetMenu (fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;

    public Sprite enemySprite;
    public Sprite enemyAttackSprite;

    public float damageAmount;

    public float healthAmount;

    // TODO: This is good, I want to refactor these to use ScriptableObjects instead of enums because we can have more than 5 types of damage
    // and we can have more than 5 weaknesses. I want to make a DamageType ScriptableObject that has a name and a weakness.
    public enum DamageType {Fire, Earth, Water, Lightning, Wind}
    public DamageType damageType;

    public enum DamageWeak { Fire, Earth, Water, Lightning, Wind }
    public DamageType damageWeak;
}
