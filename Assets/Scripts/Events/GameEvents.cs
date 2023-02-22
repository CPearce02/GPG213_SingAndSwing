using Enums;

namespace Events
{
    public static class GameEvents 
    {
        public delegate void ScreenShake(Strength str, float lengthInSeconds = 0.2f);
    
        public static ScreenShake onScreenShakeEvent;
    }
}
