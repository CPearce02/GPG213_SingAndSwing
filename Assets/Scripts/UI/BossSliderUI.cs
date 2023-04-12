using Events;

namespace UI
{
    public class BossSliderUI : SliderUI
    {
        private void OnEnable()
        {
            GameEvents.onBossHealthUIChangeEvent += ChangeSlider;
        }

        private void OnDisable()
        {
            GameEvents.onBossHealthUIChangeEvent -= ChangeSlider;
        }
        
    }
}