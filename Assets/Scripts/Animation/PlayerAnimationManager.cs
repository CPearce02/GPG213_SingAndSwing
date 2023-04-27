using UnityEngine;
using UnityEngine.InputSystem;
using Core.Player;
using GameSections.Platforming;
using System.Collections;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationManager : MonoBehaviour
    {
        Animator _animator;
        [SerializeField] private PlayerInput playerInput;
        public Rigidbody2D rb;
        public PlatformingController platformingController;
        HealthManager healthManager;

        [SerializeField][ReadOnly] bool isFalling;

        private static readonly int Falling = Animator.StringToHash("IsFalling");
        private static readonly int Grounded = Animator.StringToHash("Grounded");
        private static readonly int XVelocity = Animator.StringToHash("XVelocity");
        private static readonly int YVelocity = Animator.StringToHash("YVelocity");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Dead = Animator.StringToHash("Dead");

        #region Animations
        private static readonly int DeathAnim = Animator.StringToHash("Death");
        private static readonly int AttackAnim = Animator.StringToHash("Attack");

        private Coroutine _playerDamageFlashCoroutine;
        [SerializeField]private float _flashTime;

        [SerializeField] private Material defaultMaterial;
        private Material _flashMaterial;

        #endregion

        void Awake()
        {
            _animator = GetComponent<Animator>();
            if (playerInput == null)
                playerInput = GetComponentInParent<PlayerInput>();

            healthManager = GetComponentInParent<HealthManager>();
            _flashMaterial = Instantiate(defaultMaterial);
            GetComponent<SpriteRenderer>().material = _flashMaterial;

        }

        private void OnEnable()
        {
            playerInput.actions["Jump"].performed += SetJump;
            playerInput.actions["Attack"].performed += SetAttack;
        }

        private void OnDisable()
        {
            playerInput.actions["Jump"].performed -= SetJump;
            playerInput.actions["Attack"].performed -= SetAttack;
        }

        void Update()
        {
            SetFalling();
            SetGrounded();
            SetMovement();
            SetYVelocity();
            SetDead();
        }

        private void SetMovement()
        {
            var horizontalMovement = playerInput.actions["Move"].ReadValue<float>();
            if (platformingController.Grounded)
            {
                _animator.SetFloat(XVelocity, Mathf.Abs(horizontalMovement));
            }
        }

        private void SetYVelocity()
        {
            _animator.SetFloat(YVelocity, rb.velocity.y);
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
            if (platformingController.Grounded && context.performed) _animator.SetTrigger(Jump);
        }

        void SetAttack(InputAction.CallbackContext context) { if (context.performed && !healthManager.Dead) _animator.CrossFade(AttackAnim, 0, 0); }

        void SetDead()
        {
            _animator.SetBool(Dead, healthManager.Dead);
            if (healthManager.Dead) _animator.CrossFade(DeathAnim, 0, 0);
        }
        
        public Animator GetAnimator() => _animator;

        public void CallPlayerDamageFlash()
        {
            _playerDamageFlashCoroutine = StartCoroutine(DamageFlasher());
        }

        private IEnumerator DamageFlasher()
        {
            float currentFlashAmount = 0f;
            float elapsedTime = 0f;
            while (elapsedTime <= _flashTime)
            {
                elapsedTime += Time.fixedDeltaTime;
                currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / _flashTime));
                _flashMaterial.SetFloat("_FlashAmount", currentFlashAmount);
                yield return null;
            }
        }
    }
}
