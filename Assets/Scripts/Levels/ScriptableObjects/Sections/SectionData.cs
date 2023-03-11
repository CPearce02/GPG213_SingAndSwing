using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Levels.ScriptableObjects.Sections
{
    [CreateAssetMenu(fileName = "SectionData", menuName = "Levels/Sections/New Section Data", order = 0)]
    public class SectionData : ScriptableObject
    {
        [field: SerializeField] public string SectionName { get; private set; }
        [field: SerializeField] public string SectionDescription { get; private set; }
        [field: SerializeField] public string Scene { get; private set; }

#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;

        private void OnValidate()
        {
            if (sceneAsset != null && Scene != sceneAsset.name)
                Scene = sceneAsset.name;
        }
#endif
    }
}