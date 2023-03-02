using Events;
using Structs;
using UnityEngine;

namespace Effects
{
    public class ParticleManager : MonoBehaviour
    {
        private void OnEnable() => GameEvents.onParticleEvent += PlayParticle;

        private void OnDisable() => GameEvents.onParticleEvent -= PlayParticle;

        private void PlayParticle(ParticleEvent particleEvent)
        {
            var p = Instantiate(particleEvent.Particle, particleEvent.Transform.position, Quaternion.identity);
            var main = p.main;
            main.startColor = particleEvent.Color;
        }


    }
}
