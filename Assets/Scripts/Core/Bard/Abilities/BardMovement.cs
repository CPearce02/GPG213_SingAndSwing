using Core.Player;
using Events;
using UnityEngine;

namespace Core.Bard.Abilities
{
    public class BardMovement : MonoBehaviour
    {
        PlatformingController _knightController;
        public Transform followObj;
        public float speed = 1000f;
        [Tooltip("1 for no slowdown, 2 for LOTS of friction.")][Range(1, 2)] [SerializeField] float slowdownSpeed = 1.5f;
        public float followRange = 0.5f, maxRange = 3f;
        Rigidbody2D _rb;
        float _distance;
        Vector2 _direction;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }


        private void OnEnable()
        {
            GameEvents.onSendFollowObjectEvent += SetFollowObject;
            GameEvents.onSendPlayerEvent += SetPlayer;
        }


        private void OnDisable()
        {
            GameEvents.onSendFollowObjectEvent -= SetFollowObject;
            GameEvents.onSendPlayerEvent -= SetPlayer;
        }
        
        private void SetFollowObject(Transform tr) => followObj = tr;

        private void Update()
        {
            if(followObj)
            {
                _distance = Vector2.Distance(transform.position, followObj.position);
                _direction = (followObj.position - transform.position).normalized;
            }

            SpeedLimitAdjuster();
        }

        private void FixedUpdate()
        {
            if (_distance > followRange || _distance < -followRange)
            {
                _rb.AddForce(_direction * speed * Time.fixedDeltaTime);
            }
            else _rb.velocity /= slowdownSpeed;

            if (_distance > maxRange || _distance < -maxRange)
            {
                _rb.AddForce(_direction * speed * 5 * Time.fixedDeltaTime);
            }
        }

        void SpeedLimitAdjuster()
        {
            if (_rb.velocity.x > _knightController.speedLimit) _rb.velocity = new Vector2(_knightController.speedLimit, _rb.velocity.y);
            if (_rb.velocity.x < -_knightController.speedLimit) _rb.velocity = new Vector2(-_knightController.speedLimit, _rb.velocity.y);

            if (_rb.velocity.y > _knightController.speedLimit) _rb.velocity = new Vector2(_rb.velocity.x, _knightController.speedLimit);
            if (_rb.velocity.y < -_knightController.speedLimit) _rb.velocity = new Vector2(_rb.velocity.x, -_knightController.speedLimit);
        }

        private void SetPlayer(PlatformingController player)
        {
            _knightController = player;
        }
    }
}
