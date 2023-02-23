using System.Collections;
using Levels.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private LevelData level;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private float transitionSpeed = 1f;
        [SerializeField] private float transitionProgress = 1f;
        [SerializeField] private bool isTransitioning = false;
        private static readonly int CutOff = Shader.PropertyToID("_CutOff");
        
        private void Awake() => sprite = GetComponent<SpriteRenderer>();

        private void Update()
        {
            var mouse = Mouse.current;
            
            if (mouse.leftButton.wasPressedThisFrame && !isTransitioning)
            {
                TransitionOut();
            }
        }


        public void TransitionIn()
        {
            transitionProgress = 0;
            isTransitioning = true;
            StartCoroutine(TransitionInCoroutine());
        }
    
        public void TransitionOut()
        {
            transitionProgress = 1f;
            isTransitioning = true;
            StartCoroutine(TransitionOutCoroutine());
        }
    
        IEnumerator TransitionInCoroutine()
        {
            while (transitionProgress < 1f)
            {
                transitionProgress += Time.deltaTime * transitionSpeed;
                sprite.material.SetFloat(CutOff, transitionProgress);
                yield return null;
            }
            
            isTransitioning = false;
        }
    
        IEnumerator TransitionOutCoroutine()
        {
            while (transitionProgress > 0f)
            {
                transitionProgress -= Time.deltaTime * transitionSpeed;
                sprite.material.SetFloat(CutOff, transitionProgress);
                yield return null;
            }

            isTransitioning = false;
        }
    }
}
