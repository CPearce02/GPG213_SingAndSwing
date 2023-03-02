using Core.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu (fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;

    public Sprite enemySprite;
    public Sprite enemyAttackSprite;

    public int damageAmount;

    public int healthAmount;


    //public enum ComboKeys {ComboValue1, ComboValue2, ComboValue3, ComboValue4}

    // TODO: This is good, I want to refactor these to use ScriptableObjects instead of enums because we can have more than 5 types of damage
    // and we can have more than 5 weaknesses. I want to make a DamageType ScriptableObject that has a name and a weakness.

    //public enum DamageType { Fire, Earth, Water, Lightning, Wind }
    //public DamageType damageType;

    //public enum DamageWeak { Fire, Earth, Water, Lightning, Wind }
    //public DamageType damageWeak;

    [field: SerializeField] public DamageType DamageType { get; private set; }

    [field: SerializeField] public Combo Combo { get; private set; }


}
