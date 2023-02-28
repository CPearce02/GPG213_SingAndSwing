using Core.Player;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out HealthManager platformingController))
            platformingController.RespawnPosition = collision.transform;
    }
}
