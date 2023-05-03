using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeReference] AudioClip[] footstepSounds;
        [SerializeReference] AudioClip[] jumpSounds;
        [SerializeReference] AudioClip[] landingSounds;
        [SerializeReference] AudioClip[] attackSounds;
        [SerializeReference] AudioClip[] deathSounds;
        [SerializeReference] AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void PlayFootstepSound() => GetRandomClipFromListAndPlay(footstepSounds);

        void PlayJumpSound() => GetRandomClipFromListAndPlay(jumpSounds);
        
        void PlayLandingSound() => GetRandomClipFromListAndPlay(landingSounds);

        void PlayAttackSound() => GetRandomClipFromListAndPlay(attackSounds);
        
        void PlayDeathSound() => GetRandomClipFromListAndPlay(deathSounds);

        void GetRandomClipFromListAndPlay(AudioClip[] array)
        {
            var randomIndex = Random.Range(0, array.Length);
            audioSource.PlayOneShot(array[randomIndex]);
        }
    }
}
