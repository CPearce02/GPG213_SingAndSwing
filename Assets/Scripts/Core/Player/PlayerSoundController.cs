using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeReference] AudioClip[] footstepSounds;
        [SerializeReference] AudioClip[] jumpSounds;
        [SerializeReference] AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void PlayFootstepSound()
        {
            var randomIndex = Random.Range(0, footstepSounds.Length - 1);
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }

        void PlayJumpSound()
        {
            var randomIndex = Random.Range(0, jumpSounds.Length - 1);
            audioSource.PlayOneShot(jumpSounds[randomIndex]);
        }
    }
}
