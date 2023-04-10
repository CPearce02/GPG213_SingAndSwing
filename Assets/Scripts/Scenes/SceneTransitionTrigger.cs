using Events;
using UnityEngine;
using Animation;
using Core.Player;

namespace Scenes
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        private BoxCollider2D boxCollider;

        private void OnEnable()
        {
            GameEvents.onPlayerFreezeEvent += FreezePlayer;
        }

        private void OnDisable()
        {
            GameEvents.onPlayerFreezeEvent -= FreezePlayer;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlatformingController player);

            if (player)
            {
                //This is really bad, but its 2 hours before friday and im tired so we'll fix it later - Greg
                GameEvents.onPlayerFreezeEvent?.Invoke(player);
                GameEvents.onSceneTransitionOutEvent?.Invoke();
            }
        }

        void FreezePlayer(PlatformingController player)
        {
            player.GetComponent<PlatformingController>().enabled = false;
            player.GetComponent<Rigidbody2D>().simulated = false;
            player.GetComponentInChildren<PlayerAnimationManager>().enabled = false;
            player.GetComponentInChildren<Animator>().SetFloat("XVelocity", 0);
            player.GetComponentInChildren<Animator>().CrossFade("Landing", 0, 0);
        }

        private void OnDrawGizmos()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            var t = transform;
            Gizmos.DrawCube(t.position, boxCollider.size);
        }
    }
}
