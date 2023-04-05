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
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string LevelName { get; private set; }
        [field: SerializeField] public string LevelDescription { get; private set; }
        [field: SerializeField] public int LevelNumber { get; private set; }
        [field: SerializeField] public List<SectionData> LevelSections { get; private set; }

        [SerializeField] private SectionData currentSection;

        [SerializeField] private SectionData nextSection;

        // [SerializeField] LevelState levelState = LevelState.Inactive;

        public SectionData CurrentSection
        {
            get => currentSection;
            private set => currentSection = value;
        }

        public SectionData NextSection
        {
            get => nextSection;
            private set => nextSection = value;
        }

        #region Methods

        public void Init() => SetCurrentSection();

        public void Step() => SetCurrentSection(nextSection);

        // public void SetLevelState(LevelState state) => levelState = state;

        // public LevelState GetLevelState() => levelState;

        private void SetCurrentSection(SectionData section = null)
        {
            if (section != null)
            {
                CurrentSection = section;
            }
            else 
            {
                CurrentSection = FindCurrentSection();
            }
            
            SetNextSection();
        }

        void SetNextSection()
        {
            if (IsLastSection())
            {
                NextSection = null;
                Debug.Log("No more sections");
                return;
            }
            NextSection = LevelSections[LevelSections.IndexOf(CurrentSection) + 1];
        }
        
        public bool IsLastSection() => currentSection == LevelSections[^1];

        SectionData FindCurrentSection() => LevelSections.Find(l => l.Scene == SceneManager.GetActiveScene().name);

        #endregion
    }
}