using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/New Ability", order = 0)]
    public class AbilityData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public DamageType DamageType { get; private set; }
        
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int ManaCost { get; private set; }
        [field: SerializeField] public int Cooldown { get; private set; }
        [field: SerializeField] public int Range { get; private set; }
        [field: SerializeField] public int AreaOfEffect { get; private set; }
        [field: SerializeField] public int Duration { get; private set; }
        [field: SerializeField] public int StunDuration { get; private set; }
        [field: SerializeField] public int HealAmount { get; private set; }
        [field: SerializeField] public int HealOverTime { get; private set; }
        [field: SerializeField] public int HealOverTimeDuration { get; private set; }
    }
}