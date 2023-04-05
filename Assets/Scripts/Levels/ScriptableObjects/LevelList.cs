using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AllLevels", menuName = "Levels/New LevelList", order = 1)]
    public class LevelList : ScriptableObject
    {
        [SerializeField] List<LevelData> levels = new List<LevelData>();
        [field: SerializeField] public LevelData CurrentLevel { get; private set; }

        public List<LevelData> Levels
        {
            get => levels;
            set
            {
                levels = value;

                for (int i = 0; i < value.Count; i++)
                {
                    SetLevelName(i, value[i].LevelName);
                }
            }
        }

        private void OnDisable()
        {
            Reset();
        }

        #region Methods

        public LevelData GetLevel(int levelNumber) => levels[levelNumber];
        
        public int GetLevelNumber(LevelData level) => levels.IndexOf(level);

        public LevelData GetLevel(string levelName) => levels.Find(l => l.LevelName == levelName);

        public LevelData GetNextLevel() => levels[levels.IndexOf(CurrentLevel) + 1];
        
        public bool IsLastLevel() => levels.IndexOf(CurrentLevel) == levels.Count - 1;

        public LevelData GetCurrentLevel() => CurrentLevel;

        public void SetCurrentLevel()
        {
            CurrentLevel = levels.Find(l => l.LevelSections.Find(s => s.Scene == SceneManager.GetActiveScene().name));
        }

        public void Reset()
        {
            CurrentLevel = null;
        }

        public int GetLevelCount() => levels.Count;

        void SetLevelName(int levelNumber, string levelName) => levels[levelNumber].name = levelName;

        #endregion

#if UNITY_EDITOR

        List<LevelData> GetAllLevels()
        {
            List<LevelData> findAllLevels = AssetDatabase.FindAssets("t:LevelData")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<LevelData>)
                .OrderBy(l => l.ID)
                .ToList();
            return findAllLevels;
        }

        [ContextMenu("Update Levels")]
        void UpdateLevels()
        {
            Levels = GetAllLevels();
        }
#endif

    }
}