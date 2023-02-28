using Events;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (transform.position.y >= player.transform.position.y) GameEvents.onPlayerRespawnEvent?.Invoke();
    }
}
