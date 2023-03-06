using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Combo", menuName = "Combo")]
    public class Combo : ScriptableObject
    {
        [field: SerializeField] public List<ComboValues> ComboValues { get; private set; }
    }
}
