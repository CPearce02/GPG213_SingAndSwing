using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Core.Bard
{
    public class SlowMoController : MonoBehaviour
    {
        private PlayerInput _bardInput;

        [SerializeField] private float slowMotionTimeScale;
        private float _startTimeScale;
        private float _startFixedDeltaTime;

        [SerializeField] private int _maxTimer;
        [SerializeField] [ReadOnly] private int _currentTimeRemaining;

        Volume _slowMoVolume;

        private bool _slowMoStarted;

        [SerializeField] float lerpSpeed = 0.1f;

        public int Timer
        {
            get => _currentTimeRemaining;
            private set
            {
                _currentTimeRemaining = Mathf.Clamp(value, 0, _maxTimer);
                var normalisedTimer = Timer / (float)_maxTimer;
                GameEvents.onPlayerTimerUIChangeEvent?.Invoke(normalisedTimer);

                if (_currentTimeRemaining == 0)
                {
                    ResetTimer();
                    //Debug.Log("No more mana");
                }
            }
        }

        void Start()
        {
            //Set Time
            _startTimeScale = Time.timeScale;
            _startFixedDeltaTime = Time.fixedDeltaTime;
            //Set Mana
            Timer = _maxTimer;

            _slowMoVolume = GetComponent<Volume>();
        }

        private void OnEnable()
        {
            _bardInput = GetComponent<PlayerInput>();
            
            GameEvents.onSlowDownStart  += SlowDownTime;
            // _bardInput.actions["SlowDownButton"].performed += SlowDownTime;
            // _bardInput.actions["SlowDownButton"].canceled += ResetTimer;
        }

        private void OnDisable()
        {
            GameEvents.onSlowDownStart -= SlowDownTime;
            // _bardInput.actions["SlowDownButton"].performed -= SlowDownTime;
            // _bardInput.actions["SlowDownButton"].canceled -= ResetTimer;
        }

        private void SlowDownTime()
        {
            if(!_slowMoStarted)
            {
                Time.timeScale = slowMotionTimeScale;
                Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;
                StartCoroutine("DecreaseTime");

                //visually change screen - increase chrome
                StopCoroutine(NoSlowMoEffect());
                StartCoroutine(SlowMoEffect());
                _slowMoStarted = true;
            }
            else
            {
                ResetTimer();
                _slowMoStarted = false;
            }
        }

        private void SetOriginalTime()
        {
            Time.timeScale = _startTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime;

            //visually change screen - reset chrome
            StopCoroutine(SlowMoEffect());
            StartCoroutine(NoSlowMoEffect());
        }

        IEnumerator SlowMoEffect()
        {
            float weight = 0f;

            while(weight < 1)
            {
                weight += lerpSpeed;
                _slowMoVolume.weight = weight;
                yield return new WaitForFixedUpdate();
            }

            if (weight > 1)
            {
                _slowMoVolume.weight = 1;
            }
        }

        IEnumerator NoSlowMoEffect()
        {
            float weight = 1f;

            while (weight > 0)
            {
                weight -= lerpSpeed;
                _slowMoVolume.weight = weight;
                yield return new WaitForFixedUpdate();
            }

            if (weight < 0)
            {
                _slowMoVolume.weight = 0;
            }
        }

        private void ResetTimer()
        {
            SetOriginalTime();
            StartCoroutine("IncreaseTime");
        }

        IEnumerator DecreaseTime()
        {
            StopCoroutine("IncreaseTime");
            while (_currentTimeRemaining > 0)
            {
                Timer -= 1;
                yield return new WaitForSeconds(0.01f);
            }
        }

        IEnumerator IncreaseTime()
        {
            StopCoroutine("DecreaseTime");
            yield return new WaitForSeconds(0.5f);
            while(_currentTimeRemaining != _maxTimer)
            {
                Timer += 1;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
