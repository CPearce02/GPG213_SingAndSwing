using Events;
using Levels.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelData level;

        private void Awake() => level.Init();

        private void OnEnable()
        {
            GameEvents.onLevelLoadEvent += LoadLevel;
        }

        private void OnDisable()
        {
            GameEvents.onLevelLoadEvent -= LoadLevel;
        }

        private void LoadLevel() => SceneManager.LoadScene(level.NextSection.Scene);
    }
}