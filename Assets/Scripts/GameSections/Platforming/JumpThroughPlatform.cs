using Core.Player;
using UnityEngine;

namespace GameSections.Platforming
{
    public class JumpThroughPlatform : MonoBehaviour
    {
        Rigidbody2D _playerRb;
        PlatformingController _platformingController;

        void Start()
        {
            //TODO: This will be a problem if we have more than one player
            _playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            _platformingController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformingController>();
        }

        void Update()
        {
            // @Greg TODO: This is a rigid way of doing this. What if an enemy needs to go through a platform?
            if (_playerRb.velocity.y > 0.01f) DisableCollision();
            if (_playerRb.velocity.y <= 0)
            {
                if (_platformingController.FindGround == false)
                {
                    DisableCollision();
                    return;
                }

                EnableCollision();
            }
        }

        void EnableCollision()
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
        }

        void DisableCollision()
        {
            Physics2D.IgnoreLayerCollision(6, 7);
        }
    }
}
