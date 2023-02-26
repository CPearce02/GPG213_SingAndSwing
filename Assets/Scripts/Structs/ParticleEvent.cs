using System;
using Events;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public class ParticleEvent
    {
        [SerializeField] public Transform transform;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private bool doParticles;
        [SerializeField] private Color particleColor;

        public void Invoke()
        {
            if(!doParticles) return;
            
            GameEvents.onParticleEvent?.Invoke(particle, transform, particleColor);
        }
    }
}