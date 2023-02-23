using UnityEngine;

namespace Levels.ScriptableObjects.Sections
{
    public abstract class LevelSectionData : ScriptableObject
    {
        [field: SerializeField] public string SectionName { get; private set; }
        [field: SerializeField] public string SectionDescription { get; private set; }
        [field: SerializeField] public GameObject SegmentPrefab { get; private set; }
        [field: SerializeField] public UnityEditor.SceneAsset scene { get; private set; }
    }
}