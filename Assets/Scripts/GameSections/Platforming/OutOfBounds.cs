using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSections.Platforming
{
    public class OutOfBounds : MonoBehaviour
    {
        GameObject _player;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            //if (transform.position.y >= player.transform.position.y) GameEvents.onPlayerRespawnEvent?.Invoke();
            if (transform.position.y >= _player.transform.position.y) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
