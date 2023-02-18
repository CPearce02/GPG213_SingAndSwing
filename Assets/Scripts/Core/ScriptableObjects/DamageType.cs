using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DamageType", menuName = "DamageTypes/New DamageType", order = 0)]
    public class DamageType : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        
        [field: SerializeField] public DamageType WeaknessAgainst { get; private set; }
        [field: SerializeField] public DamageType StrongAgainst { get; private set; }
    }
}