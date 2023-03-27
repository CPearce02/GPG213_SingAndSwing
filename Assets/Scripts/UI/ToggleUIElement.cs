using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class ToggleUIElement : MonoBehaviour
    {
        [SerializeField] GameObject uiPrefab;
        [SerializeField] InputAction button;
        [SerializeField] private float animationDuration = .2f;
        [SerializeField] private float scaleFactor = 1.1f;
        
        private void OnEnable()
        {
            button.Enable();
            button.performed += Toggle;
        }

        private void OnDisable()
        {
            button.Disable();
            button.performed -= Toggle;
        }

        void Toggle(InputAction.CallbackContext context)
        {
            if (uiPrefab.activeInHierarchy)
            {
                Tween fade = uiPrefab.GetComponent<CanvasGroup>().DOFade(0, animationDuration);
                Tween scale = uiPrefab.transform.DOScale(Vector3.one / scaleFactor, animationDuration);
                Tween fadeAndScale = DOTween.Sequence().Append(fade).Join(scale);
                fadeAndScale.OnComplete(() => { uiPrefab.SetActive(false); });
            }
            else
            {
                uiPrefab.transform.localScale = Vector3.one / scaleFactor;
                uiPrefab.GetComponent<CanvasGroup>().DOFade(1, animationDuration);
                uiPrefab.transform.DOScale(Vector3.one, animationDuration);
                uiPrefab.SetActive(true);
            }
        }
        
    }
}