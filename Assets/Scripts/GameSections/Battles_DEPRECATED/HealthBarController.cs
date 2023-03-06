using UnityEngine;
using UnityEngine.UI;

namespace GameSections.Battles_DEPRECATED
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField]
        private Image healthBarSprite;
        private float reduceSpeed = 2;
        private float target = 1;

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            target = currentHealth / maxHealth;
        }

        private void Update()
        {
            healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
        }
    }
}
