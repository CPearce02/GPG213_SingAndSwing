using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace GameSections.Bard_Abilities.ScriptableObject
{
    [CreateAssetMenu(fileName = "New Combo", menuName = "Combo")]
    public class Combo : UnityEngine.ScriptableObject
    {
        [field: SerializeField] public List<ComboValues> ComboValues { get; private set; }
    }
}
