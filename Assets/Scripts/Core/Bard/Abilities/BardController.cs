using Enums;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Bard.Abilities
{
    public class BardController : MonoBehaviour
    {
        public bool Grounded { get; private set; }
        public Transform groundCheckTransform;
        public Vector2 groundCheckSize;
        public LayerMask ignoreLayers;

        private PlayerInput _bardInput;
        [SerializeField]private bool _singleplayer;


        [Header("SlowMo")]
        [SerializeField] private float slowMotionTimeScale;
        private float _startTimeScale;
        private float _startFixedDeltaTime;


        void Start()
        {
            _startTimeScale = Time.timeScale;
            _startFixedDeltaTime = Time.fixedDeltaTime;

            _bardInput = GetComponent<PlayerInput>();
            if (!_singleplayer) return;
            _bardInput.actions["SlowDownButton"].performed += ctx => SlowDownTime();
            _bardInput.actions["SlowDownButton"].canceled += ctx => ResetTime();
        }

        void Update()
        {
            GroundCheck();
        }

        private void OnButton1()
        {
            GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue1);
        }
        private void OnButton2()
        {
            GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue2);
        }
        private void OnButton3()
        {
            GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue3);
        }
        private void OnButton4()
        {
            GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue4);
        }
        private void OnAim()
        {
            GameEvents.onAimStart?.Invoke(_bardInput.actions["Aim"].ReadValue<Vector2>());
        }
        
        private void OnSlowDownButton()
        {
            GameEvents.onSlowDownStart?.Invoke();
        }
        void GroundCheck() => Grounded = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 0.1f, ~ignoreLayers);

        private void OnDrawGizmosSelected() => Gizmos.DrawWireCube(groundCheckTransform.position, groundCheckSize);

        private void SlowDownTime()
        {
            //Debug.Log("Slow");
            Time.timeScale = slowMotionTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime * slowMotionTimeScale;
        }

        private void ResetTime()
        {
            //Debug.Log("Reset");
            Time.timeScale = _startTimeScale;
            Time.fixedDeltaTime = _startFixedDeltaTime;
        }
    }
}
