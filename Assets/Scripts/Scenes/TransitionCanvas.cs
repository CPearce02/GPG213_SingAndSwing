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
            SceneManager.sceneLoaded += GetMainCamera;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= GetMainCamera;
        }

        private void GetMainCamera(Scene arg0, LoadSceneMode arg1)
        {
            if (_cam != null) return;
            
            _cam = Camera.main;
        }
    }
}
