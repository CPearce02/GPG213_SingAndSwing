using Enums;
using UnityEngine;

namespace Events
{
    public static class GameEvents 
    {
        public delegate void ScreenShake(Strength str, float lengthInSeconds = 0.2f);
        public delegate void ParticleEffect(ParticleSystem particle, Transform transform);
        public delegate void SceneTransitionOut();
        public delegate void SceneTransitionIn();
        public delegate void LoadLevel();
        public delegate void SendCamera(Camera cam);
        
        public static ScreenShake onScreenShakeEvent;
        public static SceneTransitionOut onSceneTransitionOutEvent;
        public static SceneTransitionIn onSceneTransitionInEvent;
        public static SendCamera onSendCameraEvent;
        public static ParticleEffect onParticleEvent;
        public static LoadLevel onLevelLoadEvent;
    }
}
