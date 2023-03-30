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

        void Start()
        {
            cursor = GameObject.Find("CursorToWorld");
            _au = GetComponent<AudioSource>();
            _Radius = GameObject.Find("Radius");
            _bardInput = GetComponentInParent<PlayerInput>();
            _bardInput.actions["Aim"].performed += ctx => StartSinging();
            _bardInput.actions["Aim"].canceled += ctx => EndSinging();
        }

        private void OnEnable()
        {
            GameEvents.onAimStart += AimTowards;
        }

        private void OnDisable()
        {
            GameEvents.onAimStart -= AimTowards;
        }

        private void AimTowards(Vector2 direction)
        {
            //cursorPosition = new Vector3(_bardInput.actions["Aim"].ReadValue<Vector2>().x, _bardInput.actions["Aim"].ReadValue<Vector2>().y, 10);
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

        private void StartSinging()
        {
            if(_isSinging) return ;
            _au.loop = true;
            PlayAudio(_singingVoice);
            _isSinging = true;
        }

        private void EndSinging()
        {
            _au.loop = false;
            PlayAudio(_endSingingVoice);
            _isSinging = false;
        }
        //void SetBardInput()
        //{

        //}
    }
}

