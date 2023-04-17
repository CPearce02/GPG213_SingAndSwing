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

        [SerializeField] VolumeProfile volumeProfile;
        VolumeProfile _oldVolumeProfile;

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
                    SetOriginalTime();
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
            //Set Bard Input

            InitializeOldVolume();
        }

        private void OnEnable()
        {
            _bardInput = GetComponent<PlayerInput>();

            _bardInput.actions["SlowDownButton"].performed += SlowDownTime;
            _bardInput.actions["SlowDownButton"].canceled += ResetTimer;
        }

        private void OnDisable()
        {
            _bardInput.actions["SlowDownButton"].performed -= SlowDownTime;
            _bardInput.actions["SlowDownButton"].canceled -= ResetTimer;
        }

        void InitializeOldVolume()
        {
            volumeProfile.TryGet(out Vignette vignette);
            volumeProfile.TryGet(out ChromaticAberration chrome);

            _oldVolumeProfile = ScriptableObject.CreateInstance<VolumeProfile>();
            _oldVolumeProfile.Add<Vignette>();
            _oldVolumeProfile.Add<ChromaticAberration>();

            _oldVolumeProfile.TryGet(out Vignette oldVignette);
            _oldVolumeProfile.TryGet(out ChromaticAberration oldChrome);

            oldVignette.color.Override((Color)vignette.color);
            oldVignette.intensity.Override((float)vignette.intensity);
            oldChrome.intensity.Override((float)chrome.intensity);
        }

        void SlowMoEffect()
        {
            volumeProfile.TryGet(out Vignette vignette);
            volumeProfile.TryGet(out ChromaticAberration chrome);

            if (vignette)
            {
                vignette.color.Override(Color.blue);
                vignette.intensity.Override(0.5f);
            }

            if (chrome)
            {
                chrome.intensity.Override(0.5f);
            }
        }

        void NoSlowMoEffect()
        {
            volumeProfile.TryGet(out Vignette vignette);
            volumeProfile.TryGet(out ChromaticAberration chrome);

            _oldVolumeProfile.TryGet(out Vignette oldVignette);
            _oldVolumeProfile.TryGet(out ChromaticAberration oldChrome);

            if (vignette)
            {
                vignette.color.Override((Color)oldVignette.color);
                vignette.intensity.Override((float)oldVignette.intensity);
            }

            if (chrome)
            {
                chrome.intensity.Override((float)oldChrome.intensity);
            }
        }

        private void SlowDownTime(InputAction.CallbackContext callbackContext)
        {
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;
            StartCoroutine("DecreaseTime");

            //visually change screen - increase chrome 
            SlowMoEffect();
        }

        private void SetOriginalTime()
        {
            Time.timeScale = _startTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime;

            //visually change screen - reset chrome 
            NoSlowMoEffect();
        }

        private void ResetTimer(InputAction.CallbackContext callbackContext)
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
