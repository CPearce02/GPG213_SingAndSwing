using Events;
using Levels.ScriptableObjects;
using Levels.ScriptableObjects.Sections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelList levels;
        [SerializeField] private LevelData level;
        [SerializeField] private LevelData nextLevel;
        [SerializeField][ReadOnly] private SectionData currentSection;

        private void Awake()
        {
            levels.SetCurrentLevel();
            level.Init();
        }

        private void Start()
        {
            currentSection = level.LevelSections.Find(s => s.Scene == SceneManager.GetActiveScene().name);

            CheckLastLevel();
        }

        private void CheckLastLevel()
        {
            if (!levels.IsLastLevel())
                nextLevel = levels.GetNextLevel();
            else nextLevel = null;
        }

        private void OnEnable()
        {
            GameEvents.onLevelLoadEvent += LoadLevel;
        }

        private void OnDisable()
        {
            GameEvents.onLevelLoadEvent -= LoadLevel;
            levels.Reset();
        }
        

        private void LoadLevel()
        {
            // if the section is the last section in the level then load the next level
            if (level.IsLastSection())
            {
                SceneManager.LoadScene(nextLevel.LevelSections[0].Scene);
            }
            else
            {
                SceneManager.LoadScene(level.NextSection.Scene);
                level.Step();
            }
           
        }
    }
}