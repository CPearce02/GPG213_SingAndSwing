using System.Collections;
using Enums;
using Events;
using UnityEngine;

namespace Scenes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TransitionData", menuName = "Transitions/New TransitionData", order = 0)]
    public class TransitionData : ScriptableObject
    {
        [Header("State")]
        [SerializeField] public TransitionState nextTransitionState;
        [SerializeField] public Color color;
        [SerializeField] public bool isTransitioning = false;

        [Header("Transition Settings")]
        [SerializeField] public Sprite transitionImage;
        [SerializeField] public float speed = 1f;
        [SerializeField] public float progress = 1f;
        [SerializeField] public Material material;
        [SerializeField] public float transitionInDelay = 1f, transitionOutDelay = 0.25f;

        [Header("Testing Only")]
        [SerializeField] public bool testingControls = false;

        private static readonly int CutOff = Shader.PropertyToID("_CutOff");
        private static readonly int MainColor = Shader.PropertyToID("_MainColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        #if UNITY_EDITOR
        private void OnValidate()
        {
            nextTransitionState = TransitionState.In;
        }
        
        #endif
        
        public void Init()
        {
            material.SetColor(MainColor, color);

            SetStateValues();

            if (transitionImage == null) return;

            material.SetTexture(MainTex, transitionImage.texture);
        }

        public void SetStateValues()
        {
            switch (nextTransitionState)
            {
                case TransitionState.In:
                    progress = 0;
                    material.SetFloat(CutOff, progress - 0.1f);
                    break;
                case TransitionState.Out:
                    progress = 1;
                    material.SetFloat(CutOff, progress + 0.1f);
                    break;
                default:
                    break;
            }
        }

        public IEnumerator TransitionInCoroutine(float initialDelaySeconds = 0f)
        {
            yield return new WaitForSeconds(initialDelaySeconds == 0 ? transitionInDelay : initialDelaySeconds);
            while (progress < 1f)
            {
                progress += Time.deltaTime * speed;
                SetCutOff();
                yield return null;
            }
            isTransitioning = false;
            nextTransitionState = TransitionState.Out;
        }

        public IEnumerator TransitionOutCoroutine(float initialDelaySeconds = 0f)
        {
            yield return new WaitForSeconds(initialDelaySeconds == 0 ? transitionInDelay : initialDelaySeconds);
            while (progress > 0f)
            {
                progress -= Time.deltaTime * speed;
                SetCutOff();
                yield return null;
            }
            isTransitioning = false;
            //We need to load the next level
            GameEvents.onLevelLoadEvent?.Invoke();
            nextTransitionState = TransitionState.In;
        }

        private void SetCutOff()
        {
            var transitionOffset = nextTransitionState == TransitionState.In ? 0.1f : -0.1f;
            material.SetFloat(CutOff, progress + transitionOffset);
        }

    }
}