using Core.ScriptableObjects;
using DG.Tweening;
using Events;
using UnityEngine;

namespace Core.Bard
{
    public class ComboUIAnimationController : MonoBehaviour
    {
        [SerializeField] private float animationDuration = 0.2f;

        private CanvasGroup _canvasGroup;
        
        private Tween FadeIn => _canvasGroup.DOFade(1, animationDuration);
        private Tween FadeOut => _canvasGroup.DOFade(0, animationDuration);

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            GameEvents.onNewCombo += ShowUI;
            GameEvents.onComboFinish += HideUI;
        }

        private void OnDisable()
        {
            GameEvents.onNewCombo -= ShowUI;
            GameEvents.onComboFinish -= HideUI;
        }

        void ShowUI(Combo combo)
        {
            FadeIn.Play();
        }

        void HideUI(bool complete)
        {

            FadeOut.Play();
        }

        private void OnDestroy()
        {
            FadeIn.Kill();
            FadeOut.Kill();
        }
    }
}
