using System.Collections.Generic;
using Core.ScriptableObjects;
using UnityEngine;

namespace Core.Player
{
    [CreateAssetMenu(fileName = "_Character", menuName = "Characters/New Character", order = 0)]
    public class CharacterData : ScriptableObject
    {
        
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public int Lives { get; private set; }

        [field: SerializeField] public int Mana { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float JumpSpeed { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }

        [SerializeField] public List<AbilityData> abilities = new();

        [SerializeField] public List<DamageType> combinationsUnlocked = new();

        public bool CanMove { get; private set; }
        public bool CanAttack { get; private set; }

        public void ChangeHealth(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            if(Health <= 0)
            {
                Health = 0;
            }
        }

        public void ChangeLives(int amount)
        {
            Lives += amount;
        }

    }
}