using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] [ReadOnly] private int _currentMana;

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
            //Set Time
            _startTimeScale = Time.timeScale;
            _startFixedDeltaTime = Time.fixedDeltaTime;
            //Set Mana
            Mana = _maxMana;
            //Set Bard Input
            _bardInput = GetComponent<PlayerInput>();
            if (!_singleplayer) return;
            _bardInput.actions["SlowDownButton"].performed += ctx => SlowDownTime();
            _bardInput.actions["SlowDownButton"].canceled += ctx => ResetMana();
        }

        private void SlowDownTime()
        {
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;
            ControlMana("DecreaseMana", "IncreaseMana");
        }

        private void ResetTime()
        {
            Time.timeScale = _startTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime;
        }

        private void ResetMana()
        {
            ResetTime();
            ControlMana("IncreaseMana", "DecreaseMana");
        }

        private void ControlMana(string start, string stop)
        {
            StopCoroutine(stop);
            StartCoroutine(start);
        }

        IEnumerator DecreaseMana()
        {
            while (_currentMana > 0)
            {
                Mana -= 1;
                yield return new WaitForSeconds(0.01f);
            }
        }

        IEnumerator IncreaseMana()
        {
            //StopCoroutine("DecreaseMana");
            yield return new WaitForSeconds(0.5f);
            while(_currentMana != _maxMana)
            {
                Mana += 1;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
