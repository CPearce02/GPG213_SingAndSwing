using Events;
using UnityEngine;
using Animation;
using Core.Player;
using GameSections.Platforming;

namespace Scenes
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlatformingController player);

            if (player)
            {
                //This is really bad, but its 2 hours before friday and im tired so we'll fix it later - Greg
                player.GetComponent<PlatformingController>().enabled = false;
                player.GetComponent<Rigidbody2D>().simulated = false;
                player.GetComponentInChildren<PlayerAnimationManager>().enabled = false;
                player.GetComponentInChildren<Animator>().SetFloat("XVelocity", 0);
                player.GetComponentInChildren<Animator>().CrossFade("knight_landing", 0, 0);
                GameEvents.onSceneTransitionOutEvent?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            var t = transform;
            Gizmos.DrawCube(t.position, t.localScale);
        }
    }
}
