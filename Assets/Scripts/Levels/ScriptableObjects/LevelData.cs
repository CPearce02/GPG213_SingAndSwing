using System.Collections.Generic;
using Enums;
using Levels.ScriptableObjects.Sections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Levels/New Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        [field:SerializeField] public int ID { get; private set; }
        [field:SerializeField] public string LevelName { get; private set; }
        [field:SerializeField] public string LevelDescription { get; private set; }
        [field:SerializeField] public int LevelNumber { get; private set; }
        [field:SerializeField] public List<LevelSectionData> LevelSections { get; private set; }
        [field:SerializeField] public LevelSectionData CurrentSection { get; private set; }
        [field:SerializeField] public LevelSectionData NextSection { get; private set; }
        [SerializeField] LevelState levelState = LevelState.Inactive;

        #region Methods

        public void SetLevelState(LevelState state) => levelState = state;

        public LevelState GetLevelState() => levelState;

        public void SetCurrentSection(LevelSectionData section)
        {
            CurrentSection = section;
            SetNextSection(LevelSections[LevelSections.IndexOf(section) + 1]);
        }
        
        void SetNextSection(LevelSectionData section) => NextSection = section;
        
        public void InstantiateNextSection()
        {
            if (NextSection != null)
            {
                // I need to position the next section prefab to the right of the current section prefab accounting for width of the prefab
                // Kinda like this?
                Instantiate(NextSection.SegmentPrefab, CurrentSection.SegmentPrefab.transform.position + new Vector3(NextSection.SegmentPrefab.transform.localScale.x, 0, 0), Quaternion.identity);
            }
        }

        #endregion
    }
}