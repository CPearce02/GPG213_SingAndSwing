using System.Collections;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Bard
{
    public class AimController : MonoBehaviour
    {
        private AudioSource _au;
        [SerializeField] private PlayerInput _bardInput;

        [Header("Audio")]
        [SerializeField] private AudioClip _singingVoice;
        [SerializeField] private AudioClip _endSingingVoice;
        private bool _isSinging;

        [Header("Aim Controls")]
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] private GameObject cursor;
        private Vector3 _cursorPosition;
        private GameObject _Radius;

        [Header("Mana")]
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

                if (_currentMana > 5)
                {
                    if(!_bardInput.inputIsActive)
                    {
                        //Reactivate input controls when mana is available
                        _bardInput.ActivateInput();
                    }
                }
                else if (_currentMana == 0)
                {
                    // Stop singing
                    _bardInput.DeactivateInput();
                    //Debug.Log("No more mana");
                }
            }
        }

        void Start()
        {
            //Set Mana
            Mana = _maxMana;
            //Set Cursor
            cursor = GameObject.Find("CursorToWorld");

            //Assign 
            _au = GetComponent<AudioSource>();
            _Radius = GameObject.Find("Radius");
            _bardInput = GetComponentInParent<PlayerInput>();
         
        }

        private void OnEnable()
        {
            GameEvents.onAimStart += AimTowards;
            _bardInput.actions["Aim"].performed += StartSinging;
            _bardInput.actions["Aim"].canceled +=  EndSinging;
        }

        private void OnDisable()
        {
            GameEvents.onAimStart -= AimTowards;
            _bardInput.actions["Aim"].performed -= StartSinging;
            _bardInput.actions["Aim"].canceled -=  EndSinging;
        }

        private void AimTowards(Vector2 direction)
        {
            _cursorPosition = new Vector3(direction.x, direction.y, 0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_bardInput.actions["Aim"].inProgress)
            {
                _Radius.SetActive(true);

                RotateTowards();
            }
            else
            {
                _Radius.SetActive(false);
            }
        }

        private void RotateTowards()
        {
            cursor.transform.position = Camera.main.ScreenToWorldPoint(_cursorPosition);

            Vector2 _aimDirection = cursor.transform.position - transform.position;

            float _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(_angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }

        private void PlayAudio(AudioClip ac)
        {
            _au.clip = ac;
            _au.Play();
        }

        private void StartSinging(InputAction.CallbackContext ctx)
        {
            if(_isSinging) return ;
            _au.loop = true;
            PlayAudio(_singingVoice);
            _isSinging = true;
            StartCoroutine("DecreaseMana");
        }

        private void EndSinging(InputAction.CallbackContext ctx)
        {
            _au.loop = false;
            PlayAudio(_endSingingVoice);
            _isSinging = false;
            StartCoroutine("IncreaseMana");
        }

        IEnumerator DecreaseMana()
        {
            StopCoroutine("IncreaseMana");
            while (_currentMana > 0)
            {
                Mana -= 1;
                yield return new WaitForSeconds(0.1f);
            }
        }

        IEnumerator IncreaseMana()
        {
            StopCoroutine("DecreaseMana");
            yield return new WaitForSeconds(0.5f);
            while (_currentMana != _maxMana)
            {
                Mana += 1;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

