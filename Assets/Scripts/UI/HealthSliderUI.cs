using Events;

namespace UI
{
    public class HealthSliderUI : SliderUI
    {
        private void OnEnable()
        {
            GameEvents.onPlayerHealthUIChangeEvent += ChangeSlider;
        }

        private void OnDisable()
        {
            GameEvents.onPlayerHealthUIChangeEvent -= ChangeSlider;
        }
        
    }
}