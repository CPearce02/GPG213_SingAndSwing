using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Levels.ScriptableObjects.Sections
{
    public abstract class LevelSectionData : ScriptableObject
    {
        [field: SerializeField] public string SectionName { get; private set; }
        [field: SerializeField] public string SectionDescription { get; private set; }
        //[field: SerializeField] public UnityEditor.SceneAsset Scene { get; private set; }
    }
}