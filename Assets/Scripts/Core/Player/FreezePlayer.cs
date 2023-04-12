using Animation;
using Events;
using UnityEngine;

namespace Core.Player
{
    public class FreezePlayer : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.onPlayerFreezeEvent += Freeze;
        }

        private void OnDisable()
        {
            GameEvents.onPlayerFreezeEvent -= Freeze;
        }
        
        void Freeze()
        {
            GetComponent<PlatformingController>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponentInChildren<PlayerAnimationManager>().enabled = false;
            GetComponentInChildren<Animator>().SetFloat("XVelocity", 0);
            GetComponentInChildren<Animator>().CrossFade("Landing", 0, 0);
        }
    }
}
