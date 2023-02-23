using Enums;

namespace Events
{
    public static class GameEvents 
    {
        public delegate void ScreenShake(Strength str, float lengthInSeconds = 0.2f);
        public delegate void SceneTransitionOut();
        public delegate void SceneTransitionIn();
        public delegate void LoadLevel();
        
        public static ScreenShake onScreenShakeEvent;
        public static SceneTransitionOut onSceneTransitionOutEvent;
        public static SceneTransitionIn onSceneTransitionInEvent;
        
        public static LoadLevel onLevelLoadEvent;
    }
}
