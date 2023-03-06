using UnityEngine;

namespace GameSections.Platforming
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        void Update()
        {
            transform.position = target.position + offset;
        }
    }
}
