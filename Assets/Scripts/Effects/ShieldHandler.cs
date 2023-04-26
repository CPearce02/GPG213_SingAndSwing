using Enemies;
using Structs;
using UnityEngine;
using Events;
using Core.Bard;
using Enums;
using Core.ScriptableObjects;
using System.Collections;

namespace Effects
{
    public class ShieldHandler : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        [SerializeField] Enemy enemy;
        [Header("Material Settings")]
        [SerializeField] Material defaultMaterial;
        [SerializeField] Material shieldMaterial;
        [Header("Particle Settings")]
        [SerializeField] ParticleEvent particleEvent;

        [SerializeField] float _flashTime;

        private int colourIndex = 0;

        private Coroutine _damageFlashCoroutine;
    
        private void Awake()
        {
            if(enemy == null) enemy = GetComponentInParent<Enemy>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            defaultMaterial.SetFloat("_FlashAmount", 0);
        }

        private void OnEnable()
        {
            if (enemy == null) return;
            enemy.Destroyable += SetMaterial;

        }

        private void OnDisable()
        {
            if (enemy == null) return;
            enemy.Destroyable -= SetMaterial;

        }
    
        void SetMaterial(bool canBeDestroyed)
        {
            _spriteRenderer.material = canBeDestroyed ? defaultMaterial : shieldMaterial;

            if(canBeDestroyed)
                particleEvent.Invoke();
        }

        public void ChangeColour(int colourIndex)
        {
            if (shieldMaterial.name != "Outline_ColourToCombo") return;
            if (colourIndex < enemy.enemyData.Combo.ComboValues.Count)
            {
                shieldMaterial.SetColor("_Colour", ComboDictionary.instance.comboPrefabDictionary[enemy.enemyData.Combo.ComboValues[colourIndex]].color);
            }
        }

        public void CallDamageFlash()
        {
            _damageFlashCoroutine = StartCoroutine(DamageFlasher());
        }

        private IEnumerator DamageFlasher()
        {
            float currentFlashAmount = 0f;
            float elapsedTime = 0f;
            while(elapsedTime <= _flashTime)
            {
                elapsedTime  += Time.fixedDeltaTime;
                currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime /_flashTime));
                defaultMaterial.SetFloat("_FlashAmount", currentFlashAmount);
                yield return null;
            }
        }
    }
}
