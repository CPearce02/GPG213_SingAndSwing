using System;
using System.Collections;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {

        [SerializeField] private Slider slider;
        [SerializeField] private float updateSpeedSeconds = 0.5f;

        [Header("Slider settings")]
        [SerializeField] private Color sliderWarningColor = Color.red;

        [SerializeField][ReadOnly] private Color sliderFillColor;

        [SerializeField] private float sliderWarningValue = 0.3f;
        
        Coroutine _sliderAnimationCoroutine;
        private float oldValue;
        private void Awake()
        {
            if (slider == null)
            {
                slider = GetComponentInChildren<Slider>();
            }

            sliderFillColor = slider.fillRect.gameObject.GetComponent<Image>().color;
        }

        private void OnEnable()
        {
            GameEvents.onPlayerHealthUIChangeEvent += ChangeSlider;
        }

        private void OnDisable()
        {
            GameEvents.onPlayerHealthUIChangeEvent -= ChangeSlider;
        }

        private void ChangeSlider(float normalisedValue)
        {
            if (Math.Abs(normalisedValue - oldValue) < Mathf.Epsilon)
            {
                return;
            }

            oldValue = normalisedValue;
            _sliderAnimationCoroutine = StartCoroutine(AnimateSlider(normalisedValue));
        }

        private IEnumerator AnimateSlider(float normalisedValue)
        {
            float preChangedPercent = slider.value;
            float normalisedValueFloat = normalisedValue;
            float elapsed = 0f;
            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                slider.value = Mathf.Lerp(preChangedPercent, normalisedValueFloat, elapsed / updateSpeedSeconds);
                if (slider.value <= sliderWarningValue)
                {
                    sliderFillColor = sliderWarningColor;
                }
                yield return null;
            }
            slider.value = normalisedValueFloat;
        }
    }
}
