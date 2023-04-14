using Enemies.ScriptableObjects;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] string playerUI;
        [SerializeField] private BossUIHandler bossUIPrefab;
        BossUIHandler bossUIInstance;

        private void Awake()
        {
            if (playerUI != null)
                SceneManager.LoadScene(playerUI, LoadSceneMode.Additive);
        }

        private void OnEnable()
        {
            GameEvents.onBossFightStartEvent += LoadBossUI;
            GameEvents.onBossFightEndEvent += UnloadBossUI;
        }

        private void OnDisable()
        {
            GameEvents.onBossFightStartEvent += LoadBossUI;
            GameEvents.onBossFightEndEvent -= UnloadBossUI;
        }

        private void UnloadBossUI()
        {
            Destroy(bossUIInstance.gameObject);
        }

        private void LoadBossUI(EnemyData enemyData)
        {
            if (bossUIInstance != null) return;
            bossUIInstance = Instantiate(bossUIPrefab);
            bossUIInstance.SetEnemy(enemyData);
        }
    }
}
