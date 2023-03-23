using Enemies;
using Structs;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Material _material;
    [SerializeField] Enemy enemy;
    [SerializeField] ParticleEvent particleEvent;
    [SerializeField] private Color shieldColor;
    
    private static readonly int OutlineThickness = Shader.PropertyToID("_OutlineThickness");
    private static readonly int Colour = Shader.PropertyToID("_Colour");

    private void Awake()
    {
        if(enemy == null) enemy = GetComponentInParent<Enemy>();
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        _material.SetColor(Colour, shieldColor);
    }

    private void OnEnable()
    {
        if (enemy == null) return;
        enemy.Destroyable += ExplodeShield;
    }

    private void OnDisable()
    {
        if (enemy == null) return;
        enemy.Destroyable -= ExplodeShield;
    }
    
    void ExplodeShield(bool canBeDestroyed)
    {
        if(_material != null)
            _material.SetFloat(OutlineThickness, canBeDestroyed ? 0 : 1);

        if(canBeDestroyed)
            particleEvent.Invoke();
    }
}
