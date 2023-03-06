using System;
using System.Linq;
using DG.Tweening;
using Enums;
using Events;
using Scoring.ScriptableObjects;
using Structs;
using UnityEngine;

namespace Scoring
{
    public class MultiplierUI : MonoBehaviour
    {
        
        [SerializeField] private Animator animator;
        [SerializeField] private MultiplierData multiplierData;
        [SerializeField] private ParticleSystem[] maxParticles;
        private static readonly int Multiplier = Animator.StringToHash("Multiplier");

        [SerializeField] private CameraShakeEvent[] cameraShakeEvents;
        
        Tweener tweenPunchScale;
        Tweener tweenPunchRotation;
        private void OnValidate()
        {
            if (cameraShakeEvents.Length > (int)MultiplierState.Five + 1)
            {
                cameraShakeEvents = cameraShakeEvents.Take(cameraShakeEvents.Length - 1).ToArray();
            }
        }

        private void Awake()
        {
            if (multiplierData != null) multiplierData.Init();
            animator = GetComponentInChildren<Animator>();
            
            
        }

        private void Start() => DeactivateParticles();

        private void Update() => State();

        private void OnEnable()
        {
            GameEvents.onMultiplierIncreaseEvent += multiplierData.Increment;
            GameEvents.onMultiplierIncreaseEvent += DoShake;
            GameEvents.onMultiplierDecreaseEvent += multiplierData.Decrement;
            GameEvents.onMultiplierResetEvent += multiplierData.Reset;
        }
        
        private void OnDisable()
        {
            GameEvents.onMultiplierIncreaseEvent -= multiplierData.Increment;
            GameEvents.onMultiplierIncreaseEvent -= DoShake;
            GameEvents.onMultiplierDecreaseEvent -= multiplierData.Decrement;
            GameEvents.onMultiplierResetEvent -= multiplierData.Reset;
            tweenPunchRotation?.Kill();
            tweenPunchScale?.Kill();
        }

        void State()
        {
            if (multiplierData.CurrentMultiplier != MultiplierState.Five)
                DeactivateParticles();
            else
                ActivateParticles();
            
            SetMultiplier();
        }

        void DoShake()
        {
            var state = multiplierData.CurrentMultiplier;
            float stateFloat = (float)state;
            float duration = stateFloat * .25f;
            cameraShakeEvents[(int)state].Invoke();
            tweenPunchScale = transform.DOPunchScale(Vector3.one * .25f * stateFloat, duration, 5);
            tweenPunchRotation = transform.DOPunchRotation(Vector3.forward * 2 * stateFloat, duration, 30, 30f);
        }

        void ActivateParticles()
        {
            foreach (var particle in maxParticles) particle.gameObject.SetActive(true);
        }

        void DeactivateParticles()
        {
            foreach (var particle in maxParticles) particle.gameObject.SetActive(false);
        }
        
        void SetMultiplier() => animator.SetInteger(Multiplier, (int) multiplierData.CurrentMultiplier + 1);
    }
}
