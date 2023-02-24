using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class TransitionCanvas : MonoBehaviour
    {
        private Camera _cam;
        Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

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

            canvas.worldCamera = _cam;
            Debug.Log("New camera: " + _cam);
        }
    }
}
