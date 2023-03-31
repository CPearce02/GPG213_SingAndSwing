using Events;

namespace UI
{
    public class TimerSliderUI : SliderUI
    {
        private void OnEnable()
        {
            GameEvents.onPlayerTimerUIChangeEvent += ChangeSlider;
        }

        private void OnDisable()
        {
            GameEvents.onPlayerTimerUIChangeEvent -= ChangeSlider;
        }
    }
}

