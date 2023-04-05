using Events;
using UnityEngine;

public class SendFollowObject : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.onSendFollowObjectEvent?.Invoke(transform);
    }
}
