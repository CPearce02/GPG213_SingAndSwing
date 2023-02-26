using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Effects
{
    public class ParticleManager : MonoBehaviour
    {
        private void OnEnable() => GameEvents.onParticleEvent += PlayParticle;

        private void OnDisable() => GameEvents.onParticleEvent -= PlayParticle;

        private void PlayParticle(ParticleSystem particle, Transform t) => Instantiate(particle, t.position, Quaternion.identity);
    }
}
