using System.Collections;
using Events;
using Structs;
using UnityEngine;
using Interfaces;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Core.Player
{
    public class HealthManager : MonoBehaviour, IAttackable
    {
        [SerializeField] [ReadOnly] private int health;
        [SerializeField] private CharacterData playerStats;
        [SerializeField] Transform respawnPosition;
        [SerializeField] ParticleEvent deathParticles;
        [SerializeField] private CameraShakeEvent takeDamageCameraShake;
        
        PlatformingController _controller;
        Rigidbody2D _rb;
        
        [SerializeField][ReadOnly] int maxHealth;
        bool _dead = false;

        public bool Dead { get => _dead; private set => _dead = value; }
        
        public int Health
        {
            get => health;
            private set
            {
                health = Mathf.Clamp(value, 0, maxHealth);
                var normalisedHealth = Health / (float) maxHealth; 
                GameEvents.onPlayerHealthUIChangeEvent?.Invoke(normalisedHealth);
                if (health == 0)
                {
                    GameEvents.onPlayerDiedEvent?.Invoke();
                    Debug.Log("Player died event");
                }
            }
        }
        
        public Transform RespawnPosition { get => respawnPosition; set => respawnPosition = value; }
        
        private void OnEnable()
        {
            GameEvents.onPlayerHealedEvent += IncreaseHealth;
            GameEvents.onPlayerKillEvent += KillPlayer;
            GameEvents.onPlayerRespawnEvent += Respawn;
        }
        
        private void OnDisable()
        {
            GameEvents.onPlayerHealedEvent -= IncreaseHealth;
            GameEvents.onPlayerKillEvent -= KillPlayer;
            GameEvents.onPlayerRespawnEvent -= Respawn;
        }

        void Awake()
        {
            health = playerStats.Health;
            maxHealth = health;
            respawnPosition = transform;

            _controller = GetComponent<PlatformingController>();
        }

        private void Start()
        {
            // GameEvents.onSetHealthCountEvent?.Invoke(Health);
            CreateSpawnpoint();
            _rb = GetComponent<Rigidbody2D>();
        }

        void CreateSpawnpoint()
        {
            GameObject obj = new GameObject();
            obj.name = "Spawnpoint";
            obj.transform.position = transform.position;
            respawnPosition = obj.transform;
        }

        private void ReduceHealth(int amount)
        {
            if (Health == 0) return;
            
            Health -= amount;
            takeDamageCameraShake.Invoke();
            GameEvents.onMultiplierDecreaseEvent.Invoke();
        }
        
        private void IncreaseHealth(int amount) => Health += amount;

        private void KillPlayer() => Health = 0;
        
        // Made this private because it can be called via an event :) 
        private void Respawn(float delaySeconds, Transform t)
        {
            if(t == null) 
                t = respawnPosition;
                
            if (delaySeconds == 0)
            {
                transform.position = t.position;
                Health = playerStats.Health;
            }
            else
            {
                StartCoroutine(DelayRespawn(t.position, delaySeconds));
            }
        }

        IEnumerator DelayRespawn(Vector3 respawn, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            Health = playerStats.Health;
            transform.position = respawn;
            Alive();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Linked to the interface IAttackable
        public void TakeDamage(int amount)
        {
            if (_dead) return;
            Debug.Log($"{amount} in damage was taken by the player");
            ReduceHealth(amount);

            if (health <= 0)
            {
                Death();
                Respawn(3f, respawnPosition);
            }
        }

        void Alive()
        {
            _dead = false;
            _controller.enabled = true;
            _rb.simulated = true;
        }

        void Death()
        {
            _dead = true;
            _controller.enabled = false;
            _rb.simulated = false;
            deathParticles.Invoke();
        }
    }
}
