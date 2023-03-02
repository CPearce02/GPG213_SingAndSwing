using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //if (transform.position.y >= player.transform.position.y) GameEvents.onPlayerRespawnEvent?.Invoke();
        if (transform.position.y >= player.transform.position.y) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
