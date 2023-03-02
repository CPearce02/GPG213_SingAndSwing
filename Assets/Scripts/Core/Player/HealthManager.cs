using System.Collections;
using Enums;
using Events;
using Structs;
using UnityEngine;
using Interfaces;

namespace Core.Player
{
    public class HealthManager : MonoBehaviour, IAttackable
    {
        [SerializeField] [ReadOnly] private int health;
        [SerializeField] private CharacterData playerStats;
        [SerializeField] Transform respawnPosition;
        [SerializeField] ParticleEvent deathParticles;
        PlatformingController controller;
        Rigidbody2D rb;
        
        public int maxHitPoints = 3;
        bool dead = false;

        public bool Dead { get => dead; private set => dead = value; }
        
        public int Health
        {
            get => health;
            private set
            {
                health = Mathf.Clamp(value, 0, maxHitPoints);
                GameEvents.onPlayerHealthUIChangeEvent?.Invoke(health);
                if (health == 0) GameEvents.onPlayerDiedEvent?.Invoke();
            }
        }
        
        public Transform RespawnPosition { get => respawnPosition; set => respawnPosition = value; }
        
        private void OnEnable()
        {
            GameEvents.onPlayerDamagedEvent += ReduceHealth;
            GameEvents.onPlayerHealedEvent += IncreaseHealth;
            GameEvents.onPlayerKillEvent += KillPlayer;
            GameEvents.onPlayerRespawnEvent += Respawn;
        }
        
        private void OnDisable()
        {
            GameEvents.onPlayerDamagedEvent -= ReduceHealth;
            GameEvents.onPlayerHealedEvent -= IncreaseHealth;
            GameEvents.onPlayerKillEvent -= KillPlayer;
            GameEvents.onPlayerRespawnEvent -= Respawn;
        }

        void Awake()
        {
            health = playerStats.Health;
            respawnPosition = transform;

            controller = GetComponent<PlatformingController>();
        }

        private void Start()
        {
            GameEvents.onSetHealthCountEvent?.Invoke(Health);
            CreateSpawnpoint();
            rb = GetComponent<Rigidbody2D>();
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
            _ = amount == 0 ? Health-- : Health -= amount;
            GameEvents.onScreenShakeEvent.Invoke(Strength.Medium, .2f);
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
        }

        //Linked to the interface IAttackable
        public void TakeDamage(int amount)
        {
            if (dead) return;

            ReduceHealth(amount);

            if (health <= 0)
            {
                Death();
                Respawn(3f, respawnPosition);
            }
        }

        void Alive()
        {
            dead = false;
            controller.enabled = true;
            rb.simulated = true;
        }

        void Death()
        {
            dead = true;
            controller.enabled = false;
            rb.simulated = false;
            deathParticles.Invoke();
        }
    }
}
