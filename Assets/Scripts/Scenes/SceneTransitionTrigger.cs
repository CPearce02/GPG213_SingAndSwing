using Events;
using UnityEngine;

namespace Scenes
{
    public class SceneTransitionTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlatformingController player);
            
            if(player)
                GameEvents.onSceneTransitionOutEvent?.Invoke();
        }
    }
}
