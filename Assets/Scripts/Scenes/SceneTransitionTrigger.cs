using Events;
using UnityEngine;

namespace Scenes
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlatformingController player);

            if (player)
            {
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
