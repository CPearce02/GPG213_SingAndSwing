using Events;
using Levels.ScriptableObjects;
using Scenes.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private LevelData level;
        [SerializeField] TransitionData transitionData;
        [ReadOnly][SerializeField] private Image sprite;
        
        private void Awake() => sprite = GetComponent<Image>();

        private void Start() => transitionData.Init();
        
        private void OnEnable()
        {
            GameEvents.onSceneTransitionInEvent += TransitionIn;
            GameEvents.onSceneTransitionOutEvent += TransitionOut;
            SceneManager.sceneLoaded += TransitionIn;
        }
        private void OnDisable()
        {
            GameEvents.onSceneTransitionInEvent -= TransitionIn;
            GameEvents.onSceneTransitionOutEvent -= TransitionOut;
            SceneManager.sceneLoaded -= TransitionIn;
        }

        // The mouse controls are for testing only
        private void Update()
        {
            TestTransition();
        }

        void TransitionIn()
        {
            transitionData.progress = 0;
            transitionData.isTransitioning = true;
            StartCoroutine(transitionData.TransitionInCoroutine());
        }

        void TransitionIn(Scene scene, LoadSceneMode mode)
        {
            transitionData.progress = 0;
            transitionData.isTransitioning = true;
            StartCoroutine(transitionData.TransitionInCoroutine());
        }

        void TransitionOut()
        {
            transitionData.progress = 1f;
            transitionData.isTransitioning = true;
            StartCoroutine(transitionData.TransitionOutCoroutine());
        }

        private void TestTransition()
        {
            if (!transitionData.testingControls) return;

            var mouse = Mouse.current;

            if (mouse.leftButton.wasPressedThisFrame && !transitionData.isTransitioning)
            {
                TransitionOut();
            }

            if (mouse.rightButton.wasPressedThisFrame && !transitionData.isTransitioning)
            {
                TransitionIn();
            }
        }
    }

}
