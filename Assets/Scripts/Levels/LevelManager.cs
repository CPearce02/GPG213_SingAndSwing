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
        [SerializeField][ReadOnly] private SectionData currentSection;

        private void Awake()
        {
            levels.SetCurrentLevel();
            level.Init();
        }

        private void Start()
        {
            currentSection = level.LevelSections.Find(s => s.Scene == SceneManager.GetActiveScene().name);
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
            SceneManager.LoadScene(level.NextSection.Scene);
            level.Step();
        }
    }
}