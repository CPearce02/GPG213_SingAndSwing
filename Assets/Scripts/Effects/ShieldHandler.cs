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

        private Coroutine _damageFlashCoroutine;

        private Material _damageFlashMaterial;
        private Material _colourChangeMaterial;
    
        private void Awake()
        {
            if(enemy == null) enemy = GetComponentInParent<Enemy>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
            if(_damageFlashMaterial == null || _colourChangeMaterial == null)
            {
                _damageFlashMaterial = Instantiate(defaultMaterial);
                _colourChangeMaterial = Instantiate(shieldMaterial);
            }

            _spriteRenderer.material = canBeDestroyed ? _damageFlashMaterial : _colourChangeMaterial;

            if(canBeDestroyed)
                particleEvent.Invoke();
        }

        public void ChangeColour(int colourIndex)
        {
            if (colourIndex < enemy.enemyData.Combo.ComboValues.Count)
            {
                _colourChangeMaterial.SetColor("_Colour", ComboDictionary.instance.comboPrefabDictionary[enemy.enemyData.Combo.ComboValues[colourIndex]].color);
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
                _damageFlashMaterial.SetFloat("_FlashAmount", currentFlashAmount);
                yield return null;
            }
        }
    }
}
