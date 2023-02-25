using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
                
                // This may or maynot be helpful or work, it's just so that we can see the names of the levels in the inspector
                for (int i = 0; i < value.Count; i++)
                {
                    SetLevelName(i, value[i].LevelName);
                }
            }
        }

        #region Methods
        
        public LevelData GetLevel(int levelNumber) => levels[levelNumber];
        
        public LevelData GetLevel(string levelName) => levels.Find(l => l.LevelName == levelName);
        
        public LevelData GetNextLevel() => levels[levels.IndexOf(CurrentLevel) + 1];
        
        public LevelData GetCurrentLevel() => CurrentLevel;
        
        public int GetLevelCount() => levels.Count;
        
        void SetLevelName(int levelNumber, string levelName) => levels[levelNumber].name = levelName;
        

        #endregion
        
        #if UNITY_EDITOR
        
        List<LevelData> GetLevels()
        {
            List<LevelData> findAllLevels = AssetDatabase.FindAssets("t:LevelData")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<LevelData>)
                .ToList();
            return findAllLevels;
        }
        
        [ContextMenu("Update Levels")]
        void UpdateLevels()
        {
            Levels = GetLevels();
        }
        #endif
        
    }
}