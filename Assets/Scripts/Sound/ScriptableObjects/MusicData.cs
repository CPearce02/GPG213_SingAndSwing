using System.Collections.Generic;
using UnityEngine;

namespace Sound.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MusicData", menuName = "Music/New MusicData", order = 0)]
    public class MusicData : ScriptableObject
    {
        public List<AudioClip> musicIntensities = new List<AudioClip>();
        [field: SerializeField] public float BPM { get; private set; } = 1;
    }
}
