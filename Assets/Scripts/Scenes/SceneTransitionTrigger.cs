using Events;
using UnityEngine;
using Animation;
using Core.Player;

namespace Scenes
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlatformingController player);

            if (player)
            {
                //This is really bad, but its 2 hours before friday and im tired so we'll fix it later - Greg
                GameEvents.onPlayerFreezeEvent?.Invoke();
                GameEvents.onSceneTransitionOutEvent?.Invoke();
            }
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
