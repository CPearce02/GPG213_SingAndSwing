using UnityEngine;

namespace Sound
{
    public class PlaySound : MonoBehaviour
    {
        AudioSource audioSource;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void PlayOnce()
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
