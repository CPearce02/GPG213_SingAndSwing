using UnityEngine;

namespace Sound
{
    public class PlaySoundOnCollision : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip clip;
        
        private void Awake()
        {
            if(audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(clip != null)
                audioSource.PlayOneShot(clip);
        }
    }
}
