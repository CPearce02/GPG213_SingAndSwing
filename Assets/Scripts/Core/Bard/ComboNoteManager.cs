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
        private float moveDuration = 6f;

        private void Start()
        {
            image = GetComponent<Image>();
            image.rectTransform.DOAnchorPosX(-moveDistance, moveDuration)
            .OnComplete(() => GameEvents.onComboFinish?.Invoke());
        }
    }
}
