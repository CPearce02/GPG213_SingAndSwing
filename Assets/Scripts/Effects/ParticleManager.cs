using Events;
using UnityEngine;

namespace Effects
{
    public class ParticleManager : MonoBehaviour
    {
        private void OnEnable() => GameEvents.onParticleEvent += PlayParticle;

        private void OnDisable() => GameEvents.onParticleEvent -= PlayParticle;

        private void PlayParticle(ParticleSystem particle, Transform t, Color color)
        {
            var p = Instantiate(particle, t.position, Quaternion.identity);
            var main = p.main;
            main.startColor = color;
        }
        
        
    }
}
