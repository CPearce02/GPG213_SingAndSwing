using Enemies;
using Structs;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] Enemy enemy;
    [Header("Material Settings")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material shieldMaterial;
    [Header("Particle Settings")]
    [SerializeField] ParticleEvent particleEvent;
    
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
        _spriteRenderer.material = canBeDestroyed ? defaultMaterial : shieldMaterial;

        if(canBeDestroyed)
            particleEvent.Invoke();
    }
}
