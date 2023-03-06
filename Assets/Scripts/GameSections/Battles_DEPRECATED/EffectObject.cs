using UnityEngine;

namespace GameSections.Battles_DEPRECATED
{
    public class EffectObject : MonoBehaviour
    {
        public float lifetime;
        // Start is called before the first frame update
        void Start()
        {
            //Destroy(gameObject, lifetime);
        }

        // Update is called once per frame
        void Update()
        {
            Destroy(gameObject, lifetime);

        }
    }
}
