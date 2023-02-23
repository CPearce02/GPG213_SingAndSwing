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
        [SerializeField] private bool isTransitioning = false;
        [SerializeField] private Image sprite;
        
        [Header("Transition Settings")]
        [SerializeField] Sprite transitionImage;
        [SerializeField] private float transitionSpeed = 1f;
        [SerializeField] private float transitionProgress = 1f;
        [SerializeField] private Color transitionColor;
        [SerializeField] TransitionState nextTransitionState;
        
        [Header("Testing Only")]
        [SerializeField] bool testingControls = false;
        
        private static readonly int CutOff = Shader.PropertyToID("_CutOff");
        private static readonly int MainColor = Shader.PropertyToID("_MainColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        private void Awake() => sprite = GetComponent<Image>();

        private void Start()
        {
            sprite.material.SetColor(MainColor, transitionColor);
            
            SetStateValues();
            if(transitionImage == null) return; 
            
            sprite.material.SetTexture(MainTex, transitionImage.texture);
            
        }

        private void SetStateValues()
        {
            switch (nextTransitionState)
            {
                case TransitionState.In:
                    transitionProgress = 0;
                    sprite.material.SetFloat(CutOff, transitionProgress - 0.1f);
                    break;
                case TransitionState.Out:
                    transitionProgress = 1;
                    sprite.material.SetFloat(CutOff, transitionProgress + 0.1f);
                    break;
                default:
                    break;
            }
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
            isTransitioning = true;
            StartCoroutine(TransitionInCoroutine());
        }
        
        void TransitionIn(Scene scene, LoadSceneMode mode)
        {
            transitionProgress = 0;
            isTransitioning = true;
            StartCoroutine(TransitionInCoroutine(2f));
        }
    
        void TransitionOut()
        {
            transitionProgress = 1f;
            isTransitioning = true;
            StartCoroutine(TransitionOutCoroutine());
        }

        IEnumerator TransitionInCoroutine(float initialDelaySeconds = 0f)
        {
            yield return new WaitForSeconds(initialDelaySeconds);
            while (transitionProgress < 1f)
            {
                transitionProgress += Time.deltaTime * transitionSpeed;
                SetCutOff();
                yield return null;
            }
            isTransitioning = false;
            nextTransitionState = TransitionState.Out;
        }


        IEnumerator TransitionOutCoroutine()
        {
            while (transitionProgress > 0f)
            {
                transitionProgress -= Time.deltaTime * transitionSpeed;
                SetCutOff();
                yield return null;
            }
            isTransitioning = false;
            nextTransitionState = TransitionState.In;
        }
        
        private void SetCutOff()
        {
            var transitionOffset = nextTransitionState == TransitionState.In ? 0.1f : -0.1f;
            sprite.material.SetFloat(CutOff, transitionProgress + transitionOffset);
        }
        
        private void TestTransition()
        {
            if (testingControls != true) return;

            var mouse = Mouse.current;

            if (mouse.leftButton.wasPressedThisFrame && !isTransitioning)
            {
                TransitionOut();
            }

            if (mouse.rightButton.wasPressedThisFrame && !isTransitioning)
            {
                TransitionIn();
            }
        }
    }
    
}
