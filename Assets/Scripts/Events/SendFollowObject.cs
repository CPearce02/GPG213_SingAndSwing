using UnityEngine;

namespace Events
{
    public class SendFollowObject : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.onSendFollowObjectEvent?.Invoke(transform);
        }
    }
}
