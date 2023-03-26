using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Events;
using System.Threading;

namespace Core.Bard
{
    public class SlowMoController : MonoBehaviour
    {
        private PlayerInput _bardInput;
        [SerializeField] private bool _singleplayer;

        [SerializeField] private float slowMotionTimeScale;
        private float _startTimeScale;
        private float _startFixedDeltaTime;

        [SerializeField] private int _maxMana;
        [SerializeField][ReadOnly] private int _currentMana;

        public int Mana
        {
            get => _currentMana;
            private set
            {
                _currentMana = Mathf.Clamp(value, 0, _maxMana);
                var normalisedMana = Mana / (float)_maxMana;
                GameEvents.onPlayerManaUIChangeEvent?.Invoke(normalisedMana);

                if (_currentMana == 0)
                {
                    ResetTime();
                    //Debug.Log("No more mana");
                }
            }
        }

        void Start()
        {
            _startTimeScale = Time.timeScale;
            _startFixedDeltaTime = Time.fixedDeltaTime;

            Mana = _maxMana;

            _bardInput = GetComponent<PlayerInput>();
            if (!_singleplayer) return;
            _bardInput.actions["SlowDownButton"].performed += ctx => SlowDownTime();
            _bardInput.actions["SlowDownButton"].canceled += ctx => ResetTime();
        }

        private void SlowDownTime()
        {
            //Debug.Log("Slow");
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;

            StartCoroutine("DecreaseMana");
        }

        private void ResetTime()
        {
            //Debug.Log("Reset");
            Time.timeScale = _startTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime;

            StartCoroutine("IncreaseMana");
        }

        IEnumerator DecreaseMana()
        {
            while (_currentMana != 0)
            {
                Mana -= 1;
                yield return new WaitForSeconds(0.01f);
            }
        }

        IEnumerator IncreaseMana()
        {
            StopCoroutine("DecreaseMana");
            yield return new WaitForSeconds(1f);
            while(_currentMana != _maxMana)
            {
                Mana += 1;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
