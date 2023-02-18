using System.Collections.Generic;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int Mana { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        
        [SerializeField] private List<AbilityData> abilities = new();

        public bool CanMove { get; private set; }
        public bool CanAttack { get; private set; }
        
    }
}