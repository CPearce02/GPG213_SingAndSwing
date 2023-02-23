using Events;
using UnityEngine;

namespace Scenes
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var isPlayer = col.TryGetComponent(out PlatformingController player);
            
            if(isPlayer)
                GameEvents.onSceneTransitionOutEvent?.Invoke();
        }
    }
}
