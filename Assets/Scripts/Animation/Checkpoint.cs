using Core.Player;
using Structs;
using UnityEngine;
using UnityEngine.Events;

namespace GameSections.Platforming
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] bool checkpointActive;
        [SerializeField] ParticleEvent checkpointParticles;
        Animator _anim;
        
        private static readonly int Lit = Animator.StringToHash("Lit"); 

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            checkpointActive = false;
        }
        
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(checkpointActive) return;
            if(collision.TryGetComponent(out HealthManager platformingController))
            {
                checkpointActive = true;
                HandleCheckpoint();
                HandleParticles();
                platformingController.RespawnPosition = transform;
            }
        }
        
        void HandleCheckpoint()
        {
            _anim.CrossFade(Lit, 0, 0);
        }

        void HandleParticles()
        {
            checkpointParticles.Invoke();
        }
    }
}
