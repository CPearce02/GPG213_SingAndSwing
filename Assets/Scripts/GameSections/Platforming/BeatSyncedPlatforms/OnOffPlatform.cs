using Core.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSections.Platforming.BeatSyncedPlatforms
{
    [RequireComponent(typeof(BeatListener))]
    public class OnOffPlatform : MonoBehaviour
    {
        Collider2D _collider;
        SpriteRenderer _spriteRenderer;
        Animator _animator;
        bool _toggled = false;
        bool _collidingWithPlayer = false;
        int _initialLayer;
        public float animNormalizedTime;

        private void Start()
        {

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();


            _initialLayer = gameObject.layer;
            
            _animator = GetComponent<Animator>();
            animNormalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        }

        private void Update()
        {
            animNormalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        }

        public void TogglePlatform()
        {
            //Toggle off
            if(_toggled)
            {
                DisablePlatform();
                _toggled = false;
                return;
            }

            //Toggle on
            if (!_toggled)
            {
                EnablePlatform();
                _toggled = true;
                return;
            }
        }

        void EnablePlatform()
        {
            if (!_collidingWithPlayer)
            {
                if (_animator != null) _animator.CrossFade("Restore", 0);
    

                if (_collider != null) _collider.enabled = true;
                if (_collider != null) _collider.isTrigger = false;
                gameObject.layer = _initialLayer;
            
                
            }

        }

        void DisablePlatform()
        {
            
            if (_animator != null) _animator.CrossFade("Death", 0);
           

            if (_collider != null) _collider.enabled = false;
            if (_collider != null) _collider.isTrigger = true;
            gameObject.layer = 2;
        
            

                // Debug.Log("IsDeath");
            
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            collision.transform.TryGetComponent(out PlatformingController player);
            if (player) _collidingWithPlayer = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _collidingWithPlayer = false;
        }
    }
}
