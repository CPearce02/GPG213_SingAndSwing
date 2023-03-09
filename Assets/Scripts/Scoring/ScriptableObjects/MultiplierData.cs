using Enums;
//using UnityEditor.Animations;
using UnityEngine;

namespace Scoring.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MultiplierState", menuName = "Multiplier/New MultiplierState", order = 0)]
    public class MultiplierData : ScriptableObject
    { 
        private static readonly int Multiplier = Animator.StringToHash("Multiplier");
        
        [field:SerializeField] public MultiplierState CurrentMultiplier { get; private set; } = MultiplierState.One;
        
        // We need some events that tell the UI to update the multiplier
        void ProgressMultiplier()
        {
            if (CurrentMultiplier == MultiplierState.Five) return;
            CurrentMultiplier++;
        }
        
        void DecreaseMultiplier()
        {
            if (CurrentMultiplier == MultiplierState.One) return;
            ResetMultiplier();
        }
        
        void ResetMultiplier()
        {
            CurrentMultiplier = MultiplierState.One;
        }
        
        public void Init() => ResetMultiplier();
        
        public void Increment() => ProgressMultiplier();
        
        public void Decrement() => DecreaseMultiplier();
        
        public void Reset() => ResetMultiplier();
        
    
    }
}