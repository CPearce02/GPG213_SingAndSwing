using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class TransitionCanvas : MonoBehaviour
    {
        private Camera _cam;
        Canvas _canvas;

        private void Awake() => _canvas = GetComponent<Canvas>();

        private void OnEnable()
        {
            SceneManager.sceneLoaded += GetMainCamera;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= GetMainCamera;
        }

        private void GetMainCamera(Scene scene, LoadSceneMode mode)
        {
            if (_cam != null) return;
            
            _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            _canvas.worldCamera = _cam;
            Debug.Log("New camera: " + _cam);
        }
    }
}
