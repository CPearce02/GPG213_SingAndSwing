using Core.Player;
using Structs;
using UnityEngine;

namespace Animation
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] bool isCheckpoint;
        [SerializeField] bool checkpointActive;
        [SerializeField] ParticleEvent checkpointParticles;
        Animator _anim;
        
        private static readonly int Lit = Animator.StringToHash("Lit"); 

        private void Awake()
        {
            _anim = GetComponentInChildren<Animator>();
            if(!isCheckpoint) checkpointActive = false;
        }

        private void Start()
        {
            if (!isCheckpoint)
            {
                HandleCheckpoint();
                HandleParticles();
            } else 
                checkpointActive = false;
        }
        
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!isCheckpoint) return;
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
