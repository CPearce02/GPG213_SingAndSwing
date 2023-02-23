using System.Collections.Generic;
using System.ComponentModel;
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
        
        [SerializeField] private LevelSectionData currentSection;
        
        [SerializeField] private LevelSectionData nextSection;
        
        [SerializeField] LevelState levelState = LevelState.Inactive;
        
        public LevelSectionData CurrentSection
        {
            get => currentSection;
            private set => currentSection = value;
        }

        public LevelSectionData NextSection
        {
            get => nextSection;
            private set => nextSection = value;
        }

        #region Methods
        
        public void Init() => SetCurrentSection();
        
        public void Step() => SetCurrentSection(nextSection);
        
        public void SetLevelState(LevelState state) => levelState = state;

        public LevelState GetLevelState() => levelState;

        private void SetCurrentSection(LevelSectionData section = null)
        {
            if (currentSection == null)
            {
                CurrentSection = FindCurrentSection();
            } 
            else if(section != null)
            {
                CurrentSection = section;
            }
            
            SetNextSection();
        }
        
        void SetNextSection()
        {
            if (LevelSections.IndexOf(CurrentSection) + 1 > LevelSections.Count - 1)
            {
                NextSection = null;
                Debug.Log("No more sections");
                return;
            }
            nextSection = LevelSections[LevelSections.IndexOf(CurrentSection) + 1];
        }

        LevelSectionData FindCurrentSection() => LevelSections.Find(l => l.Scene.name == SceneManager.GetActiveScene().name);

        #endregion
    }
}