using Core.Bard.Abilities;
using Core.Player;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Animation
{
    public class BardSingleplayerAnimator : MonoBehaviour
    {
        [SerializeField][ReadOnly] private Transform playerTransform;
        [SerializeField][ReadOnly] private PlayerInput playerInput;
        [SerializeField] private PlayerInput bardInput;

        Rigidbody2D _rb;
        SpriteRenderer _bardSprite;
        [FormerlySerializedAs("_animator")] [SerializeField] Animator animator;
        BardController _bardController;

        Vector2 _lastPos;

        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Falling = Animator.StringToHash("Falling");
        private static readonly int IsSinging = Animator.StringToHash("IsSinging");

        void Awake()
        {
            _bardSprite = GetComponent<SpriteRenderer>();
            if(bardInput == null)
                bardInput = GetComponentInParent<PlayerInput>();
            if(animator == null)
                animator = GetComponent<Animator>();
            _rb = GetComponentInParent<Rigidbody2D>();
            _bardController = GetComponentInParent<BardController>();
        }

        private void Start() => GameEvents.onRequestPlayerEvent?.Invoke();

        private void OnEnable()
        {
            GameEvents.onSendPlayerEvent += SetPlayer;
            bardInput.actions["Aim"].performed += SetSinging;
            bardInput.actions["Aim"].canceled += EndSinging;
        }

        private void EndSinging(InputAction.CallbackContext context)
        {
            animator.SetBool(IsSinging, false);
        }

        private void SetSinging(InputAction.CallbackContext context)
        {
            if (context.performed) animator.SetBool(IsSinging, true);
        }
        
        private void OnDisable()
        {
            GameEvents.onSendPlayerEvent -= SetPlayer;
            if (playerInput != null)
                playerInput.actions["Jump"].performed -= SetJump;
            
            bardInput.actions["Aim"].performed -= SetSinging;
            bardInput.actions["Aim"].canceled -= EndSinging;
        }

        private void SetPlayer(PlatformingController player)
        {
            playerInput = player.PlayerInput;
            playerTransform = player.transform;
            playerInput.actions["Jump"].performed += SetJump;
        }

        void Update()
        {
            FlipSprite();
            SetFalling();
            SetRunning();

            animator.SetBool(Grounded, _bardController.Grounded);
        }

        private void LateUpdate()
        {
            _lastPos = transform.position;
        }

        void FlipSprite()
        {
            float distanceFromPlayer = playerTransform.position.x - transform.position.x;

            if (distanceFromPlayer >= 0) _bardSprite.flipX = false;
            else _bardSprite.flipX = true;
        }

        void SetFalling()
        {
            if (transform.position.y < _lastPos.y - 0.1f) animator.SetBool(Falling, true);
            else animator.SetBool(Falling, false);
        }

        void SetRunning()
        {
            if (transform.position.x > _lastPos.x + 0.1f || transform.position.x < _lastPos.x - 0.1f)
            {
                if (!_bardController.Grounded) return;
                animator.SetBool(Moving, true);
            }
            else
            {
                animator.SetBool(Moving, false);
            }
        }

        void SetJump(InputAction.CallbackContext context)
        {
            if (_bardController.Grounded && context.performed) animator.SetTrigger(Jump);
        }
    }
}
