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

        //particleEvent 

        void Start()
        {
            _bardInput = GetComponent<PlayerInput>();
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

        void GroundCheck() => Grounded = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 0.1f, ~ignoreLayers);

        private void OnDrawGizmosSelected() => Gizmos.DrawWireCube(groundCheckTransform.position, groundCheckSize);
    }
}
