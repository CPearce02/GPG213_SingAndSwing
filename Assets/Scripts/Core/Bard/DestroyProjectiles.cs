using UnityEngine;
using Structs;
using Enemies;

public class DestroyProjectiles : MonoBehaviour
{
    [SerializeField] ParticleEvent takeDamageParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<ProjectileController>(out ProjectileController pc))
        {
            pc.DestroyBullet();
        }
    }
}
