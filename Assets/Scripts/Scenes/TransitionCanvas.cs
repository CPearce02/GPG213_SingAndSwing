using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class TransitionCanvas : MonoBehaviour
    {
        private Camera _cam;
        
        void Start() => DontDestroyOnLoad(gameObject);

        private void OnEnable()
        {
            GameEvents.onSendCameraEvent += GetMainCamera;
        }

        private void OnDisable()
        {
            GameEvents.onSendCameraEvent -= GetMainCamera;
        }

        private void GetMainCamera(Camera cam)
        {
            if (_cam != null) return;
            
            _cam = Camera.main;
        }
    }
}
