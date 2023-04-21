using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class AssignUICamera : MonoBehaviour
    {
        private Camera _cam;
        Canvas _canvas;

        private void Awake() => _canvas = GetComponent<Canvas>();

        private void Start()
        {
            if (_cam == null) FindCamera();
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
            FindCamera();
        }

        private void FindCamera()
        {
            _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            _canvas.worldCamera = _cam;
            Debug.Log("New camera: " + _cam);
        }
    }
}
