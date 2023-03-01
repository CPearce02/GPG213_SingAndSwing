using Enums;
using Structs;
using UnityEngine;

namespace Events
{
    public static class GameEvents 
    {
        public delegate void ScreenShake(Strength str, float lengthInSeconds = 0.2f);
        public delegate void ParticleEffect(ParticleEvent particleEvent);
        public delegate void SceneTransitionOut();
        public delegate void SceneTransitionIn();
        public delegate void LoadLevel();
        public delegate void SendCamera(Camera cam);
        public delegate void ButtonPressed(ComboValues comboValue);
        public delegate void NewCombo();


        #region Player Events
        public delegate void PlayerKill();
        public delegate void PlayerTakeDamage(int amount);   
        public delegate void PlayerHeal(int amount);
        public delegate void PlayerDied();
        public delegate void PlayerRespawn(float delaySeconds = 0, Transform positionToRespawn = null);
        
        public static PlayerTakeDamage onPlayerDamagedEvent;
        public static PlayerHeal onPlayerHealedEvent;
        public static PlayerKill onPlayerKillEvent;
        public static PlayerDied onPlayerDiedEvent;
        public static PlayerRespawn onPlayerRespawnEvent;
        #endregion
        public static ButtonPressed onButtonPressed;
        public static NewCombo onNewCombo;

        #region UI Events
        public delegate void SetValue(int value);
        public delegate void PlayerHealthUIChange(int currentHealth);
        public static SetValue onSetHealthCountEvent;
        public static PlayerHealthUIChange onPlayerHealthUIChangeEvent;
        #endregion
        
        public static ScreenShake onScreenShakeEvent;
        public static SceneTransitionOut onSceneTransitionOutEvent;
        public static SceneTransitionIn onSceneTransitionInEvent;
        public static SendCamera onSendCameraEvent;
        public static ParticleEffect onParticleEvent;
        public static LoadLevel onLevelLoadEvent;
    }
}
