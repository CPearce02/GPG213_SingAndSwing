using Structs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationManager : MonoBehaviour
    {
        Animator _animator;
        [SerializeField] private PlayerInput playerInput;
        public Rigidbody2D rb;
        public PlatformingController platformingController;
    
        [SerializeField] [ReadOnly] bool isFalling;

        [Header("Camera Shake Events")] 
        
        [SerializeField] private CameraShakeEvent shakeOnLanded = new ();
    
        private static readonly int Falling = Animator.StringToHash("IsFalling");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int XVelocity = Animator.StringToHash("XVelocity");
        private static readonly int Jump = Animator.StringToHash("Jump");

        void Awake()
        {
            _animator = GetComponent<Animator>();
            if (playerInput == null)
                playerInput = GetComponentInParent<PlayerInput>();
        }

        private void OnEnable()
        {
            playerInput.actions["Jump"].performed += SetJump;
        }

        private void OnDisable()
        {
            playerInput.actions["Jump"].performed -= SetJump;
        }

        void Update()
        {
            SetFalling();
            SetGrounded();
        }

        void SetFalling()
        {
            if (platformingController.Grounded) return;

            isFalling = rb.velocity.y < 0;
            _animator.SetBool(Falling, isFalling);
        }

        void SetGrounded() => _animator.SetBool(Grounded, platformingController.Grounded);

        void SetJump(InputAction.CallbackContext context)
        {
            if(platformingController.Grounded) _animator.SetTrigger(Jump);
        }

        void InvokeLanded()
        {
            shakeOnLanded.Invoke();
        }
    }
}
