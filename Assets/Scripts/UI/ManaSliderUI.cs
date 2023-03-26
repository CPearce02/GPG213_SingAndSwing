using Events;

namespace UI
{
    public class ManaSliderUI : SliderUI
    {
        private void OnEnable()
        {
            GameEvents.onPlayerManaUIChangeEvent += ChangeSlider;
        }

        private void OnDisable()
        {
            GameEvents.onPlayerManaUIChangeEvent -= ChangeSlider;
        }
        
    }
}