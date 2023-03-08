using Core.ScriptableObjects;
using Enums;
using Structs;
using UnityEngine;

namespace Events
{
    public static class GameEvents 
    {
        
       

        #region Player Events
        public delegate void PlayerKill();
        public delegate void PlayerTakeDamage(int amount);   
        public delegate void PlayerHeal(int amount);
        public delegate void PlayerDied();
        public delegate void PlayerRespawn(float delaySeconds = 0, Transform positionToRespawn = null);
        
        public static PlayerHeal onPlayerHealedEvent;
        public static PlayerKill onPlayerKillEvent;
        public static PlayerDied onPlayerDiedEvent;
        public static PlayerRespawn onPlayerRespawnEvent;
        #endregion

        #region Combos
        public delegate void ButtonPressed(ComboValues comboValue);
        public delegate void NewCombo(Combo combo);
        public delegate void ComboFinished ();
        public delegate void AimStart(Vector2 directon);
        
        public static ButtonPressed onButtonPressed;
        public static NewCombo onNewCombo;
        public static ComboFinished onComboFinish;
        public static AimStart onAimStart;
        #endregion

        #region UI Events
        public delegate void SetValue(int value);
        public delegate void PlayerHealthUIChange(float normalisedCurrentHealth);
        
        public static SetValue onSetHealthCountEvent;
        public static PlayerHealthUIChange onPlayerHealthUIChangeEvent;
        #endregion

        #region Effects
        public delegate void SceneTransitionOut();
        public delegate void SceneTransitionIn();
        public delegate void ScreenShake(Strength str, float lengthInSeconds = 0.2f);
        public delegate void ParticleEffect(ParticleEvent particleEvent);
        public delegate void LoadLevel();
        public delegate void SendCamera(Camera cam);

        public static ScreenShake onScreenShakeEvent;
        public static SceneTransitionOut onSceneTransitionOutEvent;
        public static SceneTransitionIn onSceneTransitionInEvent;
        public static SendCamera onSendCameraEvent;
        public static ParticleEffect onParticleEvent;
        public static LoadLevel onLevelLoadEvent;
        #endregion

        #region Score
        public delegate void ScoreChange(int score);
        public delegate void MultiplierIncrease();
        public delegate void MultiplierReset();
        public delegate void MultiplierDecrease();
        
        public static ScoreChange onScoreChangeEvent;
        public static MultiplierIncrease onMultiplierIncreaseEvent;
        public static MultiplierReset onMultiplierResetEvent;
        public static MultiplierDecrease onMultiplierDecreaseEvent;
        #endregion
    }
}
