using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class TrailHandler
    {
        [SerializeField] private TrailRenderer trail;
        
        public void DisableTrail() => trail.enabled = false;
    }
}