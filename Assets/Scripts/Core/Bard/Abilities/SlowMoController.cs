using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
            _bardInput = GetComponent<PlayerInput>();
            _bardInput.actions["SlowDownButton"].performed += ctx => SlowDownTime();
            _bardInput.actions["SlowDownButton"].canceled += ctx => ResetTimer();
        }



        private void SlowDownTime()
        {
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;
            StartCoroutine("DecreaseTime");

            //visually change screen - increase chrome 
        }

        private void SetOriginalTime()
        {
            Time.timeScale = _startTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime;

            //visually change screen - reset chrome 

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
