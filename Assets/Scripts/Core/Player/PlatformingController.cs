using Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public class PlatformingController : MonoBehaviour
    {
        float MoveSpeed => playerStats.MoveSpeed;
        float JumpSpeed => playerStats.JumpSpeed;
        float JumpHeight => playerStats.JumpHeight;

        float _relativeJumpHeight = 0;

        [Range(1, 2)] public float friction;
        public float speedLimit = 1000f;

        PlayerInput _playerInput;
        Rigidbody2D _rb;
        public CharacterData playerStats;

        public Transform groundCheckTransform;
        public Transform roofCheckTransform;
        public LayerMask ignoreLayers;
        public Vector2 groundCheckSize;

        Transform _platformTarget;
        Vector2 _platformOffset;

        //holdingJump is used while the player is jumping, jumped is when the player has finished their jump.
        bool _holdingJump = false, _jumped = false;

        bool _grounded = false, _findGround = false, _touchingRoof = false;
        public bool Grounded { get => _grounded; private set => _grounded = value; }
        public bool FindGround { get => _findGround; private set => _findGround = value; }
        public bool TouchingRoof { get => _touchingRoof; private set => _touchingRoof = value; }
        public PlayerInput PlayerInput { get => _playerInput; private set => _playerInput = value; }

        bool _allowFriction = false;


        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            PlayerInput = GetComponent<PlayerInput>();
        }


        private void Update()
        {
            OnMove();
            OnJump();

            FindingGround(); //Different to CheckGround(), because it checks further down and CheckGround() only activates when the player is colliding with something.
            CheckRoof();
        }

        private void FixedUpdate()
        {
            if (_allowFriction) AddFriction();
        }

        private void LateUpdate()
        {
            if (_platformTarget != null)
            {
                var position = _platformTarget.transform.position;
                transform.position = new Vector2(position.x, position.y) + _platformOffset;
            }
        }

        void OnMove()
        {
            float direction = PlayerInput.actions["Move"].ReadValue<float>();
            bool isPressingMove = direction != 0;

            CapSpeed();

            if (isPressingMove)
            {
                _allowFriction = false;

                //If the player is moving in the opposite direction and presses a different key and is on the ground, reset their x velocity.
                if (Grounded)
                {
                    if (direction > 0 && _rb.velocity.x < 0) _rb.velocity = new Vector2(0, _rb.velocity.y);
                    if (direction < 0 && _rb.velocity.x > 0) _rb.velocity = new Vector2(0, _rb.velocity.y);
                }

                _rb.AddForce(new Vector2(MoveSpeed * direction * Time.deltaTime, 0));
            }
            else
            {
                _allowFriction = true;
            }
        }

        void AddFriction() => _rb.velocity = new Vector2(_rb.velocity.x / friction, _rb.velocity.y);

        void CapSpeed()
        {
            if (_rb.velocity.x > speedLimit) _rb.velocity = new Vector2(speedLimit, _rb.velocity.y);
            if (_rb.velocity.x < -speedLimit) _rb.velocity = new Vector2(-speedLimit, _rb.velocity.y);
        }

        void OnJump()
        {
            var jump = PlayerInput.actions["Jump"];
            float pressing = jump.ReadValue<float>();
            bool isPressingJump = pressing != 0;

            if (Grounded) CalculateJumpHeight();

            if (isPressingJump && _jumped == false)
            {
                //If the player wasn't already jumping, and they are on the ground, let them call the jump function.
                if (Grounded && !_holdingJump) _holdingJump = true;

                if (_holdingJump) Jump();

                if (transform.position.y > _relativeJumpHeight || TouchingRoof)
                {
                    _holdingJump = false; //Stop the player from continuing their jump if they finish their jump.
                    _jumped = true; //Prevent bunnyhopping.
                }
            }

            if (!isPressingJump)
            {
                //Stop the player from continuing their jump if they let go of the jump key.
                _holdingJump = false;

                //Prevent bunnyhopping
                if (Grounded) _jumped = false;
                else _jumped = true;
            }
        }

        void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(transform.up * JumpSpeed, ForceMode2D.Impulse);
        }

        //Public void so it can be called in other scripts such as a jump pad.
        public void AddJump(float jumpHeight = 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        }

        void CheckRoof() => TouchingRoof = Physics2D.BoxCast(roofCheckTransform.position, groundCheckSize, 0f, Vector2.up, 0.1f, ~ignoreLayers);
        void CheckGround() => Grounded = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 0.1f, ~ignoreLayers);
        void FindingGround() => _findGround = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 1f, ~ignoreLayers);

        void CalculateJumpHeight() => _relativeJumpHeight = JumpHeight + transform.position.y;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            transform.SetParent(collision.transform);
            //BUG: if i run into an enemy this makes me a child of the enemy. Could be a good idea to stomp this bug and perhaps remove this.
        }

        //Check ground only when the player is touching something.
        private void OnCollisionStay2D(Collision2D collision)
        {
            CheckGround();

            _platformTarget = collision.transform;
            _platformOffset = transform.position - collision.transform.position;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Grounded = false;

            _platformTarget = null;

            transform.SetParent(null);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(groundCheckTransform.position, groundCheckSize);
        }

        // We might need to refactor when we have two players
        private void SendPlayer() => GameEvents.onSendPlayerEvent?.Invoke(this);
    }
}