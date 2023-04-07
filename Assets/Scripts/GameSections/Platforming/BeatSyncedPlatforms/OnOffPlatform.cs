using Core.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSections.Platforming.BeatSyncedPlatforms
{
    [RequireComponent(typeof(BeatListener))]
    public class OnOffPlatform : MonoBehaviour
    {
        [FormerlySerializedAs("_onColor")] [SerializeField] Color onColor;
        [SerializeField] Color offColor = Color.grey;
        Collider2D _collider;
        SpriteRenderer _spriteRenderer;
        bool _toggled = false;
        bool _collidingWithPlayer = false;
        int _initialLayer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            onColor = _spriteRenderer.color;

            _initialLayer = gameObject.layer;
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
                if (_collider != null) _collider.enabled = true;
                if (_collider != null) _collider.isTrigger = false;
                gameObject.layer = _initialLayer;
            }

            if (_spriteRenderer != null) _spriteRenderer.color = onColor;
        }

        void DisablePlatform()
        {
            if (_collider != null) _collider.enabled = false;
            if (_collider != null) _collider.isTrigger = true;
            if (_collider != null) _collider.enabled = true;

            gameObject.layer = 2;

            if (_spriteRenderer != null) _spriteRenderer.color = offColor;
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
