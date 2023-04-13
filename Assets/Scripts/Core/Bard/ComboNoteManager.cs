using Enums;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Events;

namespace Core.Bard
{
    public class ComboNoteManager : MonoBehaviour
    {
        public ComboValues value;

        private Image image;
        private float moveDistance = 47f;
        private float moveDuration = 3f;
        Tween translate;

        public bool _beenPressed;

        private void Start()
        {
            image = GetComponent<Image>();
            translate = image.rectTransform.DOAnchorPosX(-moveDistance, moveDuration)
            .OnComplete(CheckIfPressed);
        }

        void OnDisable() {
            translate.Kill();
        }
        private void CheckIfPressed()
        {
            if(_beenPressed)
            {
                
            }
            else
            {
                GameEvents.onComboFinish?.Invoke(false);
            }
        }

        public void SetMoveDuration(float value) => moveDuration = value;
    }
}
