using Core.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Events;

namespace GameSections.Platforming
{
    public class OutOfBounds : MonoBehaviour
    {
        PlatformingController _player;
        [SerializeField] Enemies.Enemy boss;
        bool _bossDead = false;

        private void OnEnable()
        {
            if (boss == null) return;
            boss.BossDeath += IfBossDead;
        }

        private void OnDisable()
        {
            if (boss == null) return;
            boss.BossDeath -= IfBossDead;
        }

        void Start()
        {
            // I added the player tag to the bard also so I used this instead of the Tag
            // But what if the bard falls?
            _player = FindObjectOfType<PlatformingController>().TryGetComponent(out PlatformingController player) ? player : null;
        }
        void Update()
        {
            // We should be using events and manage this stuff in a GameManager
            //if (transform.position.y >= player.transform.position.y) GameEvents.onPlayerRespawnEvent?.Invoke();
            if (transform.position.y >= _player.transform.position.y)
            {
                switch(_bossDead)
                {
                    case false:
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        break;

                    case true:
                        SceneManager.LoadScene("EndLevel");
                        break;
                }
            }
        }

        void IfBossDead() => _bossDead = true;
    }
}
