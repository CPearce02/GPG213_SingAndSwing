using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneTransitionTrigger : MonoBehaviour
    {
        [SerializeField] private string levelToLoad;

        private void OnTriggerEnter2D(Collider2D col)
        {
            col.TryGetComponent(out PlatformingController player);

            if (player)
            {
                SceneManager.LoadScene(levelToLoad);
            // GameEvents.onSceneTransitionOutEvent?.Invoke();
            }
        }
    }
}
