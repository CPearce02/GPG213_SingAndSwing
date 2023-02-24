using System.Collections;
using Enums;
using Events;
using Levels.ScriptableObjects;
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
        [SerializeField] private Image sprite;

        [Header("Transition Settings")]
        [SerializeField] Sprite transitionImage;
        [SerializeField] private float transitionSpeed => transitionData.speed;
        [SerializeField] private float transitionProgress = 1f;
        [SerializeField] TransitionState nextTransitionState;

        [SerializeField] public bool c { get; private set; }

        private static readonly int CutOff = Shader.PropertyToID("_CutOff");
        private static readonly int MainColor = Shader.PropertyToID("_MainColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        private void Awake() => sprite = GetComponent<Image>();

        private void Start()
        {
            transitionData.material.SetColor(MainColor, transitionData.color);

            transitionData.SetStateValues();

            if (transitionImage == null) return;

            transitionData.material.SetTexture(MainTex, transitionData.transitionImage.texture);
        }

        // The mouse controls are for testing only
        private void Update()
        {
            TestTransition();
        }

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


        void TransitionIn()
        {
            transitionProgress = 0;
            transitionData.isTransitioning = true;
            StartCoroutine(transitionData.TransitionInCoroutine());
        }

        void TransitionIn(Scene scene, LoadSceneMode mode)
        {
            transitionData.progress = 0;
            transitionData.isTransitioning = true;
            StartCoroutine(transitionData.TransitionInCoroutine(2f));
        }

        void TransitionOut()
        {
            transitionData.progress = 1f;
            transitionData.isTransitioning = true;
            StartCoroutine(transitionData.TransitionOutCoroutine());
        }

        private void TestTransition()
        {
            if (transitionData.testingControls != true) return;

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
