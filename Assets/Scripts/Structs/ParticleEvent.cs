using System;
using Events;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public struct ParticleEvent
    {
        public Transform transform;
        public ParticleSystem particle;
        public bool doParticles;

        public ParticleEvent(ParticleSystem particle, Transform transform, bool doParticles)
        {
            this.doParticles = doParticles;
            this.particle = particle;
            this.transform = transform;
        }
        
        public void Invoke()
        {
            if (!doParticles) return;
            GameEvents.onParticleEvent?.Invoke(particle, transform);
        }
    }
}