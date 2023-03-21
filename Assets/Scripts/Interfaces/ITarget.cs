using UnityEngine;

namespace Interfaces
{
    public interface ITarget
    {
        public void SetTarget(Transform target);
        public void RemoveTarget();
    }
}