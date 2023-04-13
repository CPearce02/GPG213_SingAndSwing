using Core.Player;
using Core.ScriptableObjects;
using Enemies.ScriptableObjects;
using Enums;
using Enemies;
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
        public delegate void PlayerFreeze();

        public static PlayerHeal onPlayerHealedEvent;
        public static PlayerKill onPlayerKillEvent;
        public static PlayerDied onPlayerDiedEvent;
        public static PlayerRespawn onPlayerRespawnEvent;
        public static PlayerFreeze onPlayerFreezeEvent;
        #endregion

        #region Utility Events
        public delegate void RequestPlayer();
        public delegate void SendPlayer(PlatformingController player);
        public delegate void SendFollowObject(Transform transform);
        
        public static RequestPlayer onRequestPlayerEvent;
        public static SendPlayer onSendPlayerEvent;
        public static SendFollowObject onSendFollowObjectEvent;
        #endregion

        #region Music Events
        public delegate void BeatFired();
        public static BeatFired onBeatFiredEvent;
        #endregion

        #region Combos
        public delegate void ButtonPressed(ComboValues comboValue);
        public delegate void CorrectButtonPressed();
        public delegate void WrongButtonPressed();
        public delegate void NewCombo(Combo combo);
        public delegate void ComboFinished(bool complete);
        public delegate void AimStart(Vector2 directon);
        public delegate void SlowDownStart();

        public static ButtonPressed onButtonPressed;
        public static CorrectButtonPressed onCorrectButtonPressed;
        public static WrongButtonPressed onWrongButtonPressed;
        public static NewCombo onNewCombo;
        public static ComboFinished onComboFinish;
        public static AimStart onAimStart;
        public static SlowDownStart onSlowDownStart;
  
        #endregion

        #region UI Events
        
        public delegate void SetValue(int value);
        public delegate void PlayerHealthUIChange(float normalisedCurrentHealth);
        public delegate void PlayerManaUIChange(float normalisedCurrentMana);
        public delegate void PlayerTimerUIChange(float normalisedCurrentTimer);
        public delegate void BossHealthUIChange(float normalisedCurrentHealth);

        public delegate void BossFightStart(EnemyData enemyData);
        public delegate void BossFightEnd();

        public delegate void TargetEnemy(Enemy enemy);

        public static TargetEnemy onTargetEnemyEvent;
        public static SetValue onSetHealthCountEvent;
        public static PlayerHealthUIChange onPlayerHealthUIChangeEvent;
        public static PlayerManaUIChange onPlayerManaUIChangeEvent;
        public static PlayerTimerUIChange onPlayerTimerUIChangeEvent;
        
        public static BossHealthUIChange onBossHealthUIChangeEvent;

        public static BossFightStart onBossFightStartEvent;
        public static BossFightEnd onBossFightEndEvent;

        #endregion

        #region Effects
        public delegate void SceneTransitionIn();
        public delegate void SceneTransitionOut();
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
