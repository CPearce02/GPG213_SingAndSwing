using UnityEngine;

namespace GameSections.Bard_Abilities
{
    public class EnemySpriteManager : MonoBehaviour
    {
        private SpriteRenderer sr;
        private EnemyPlatforming ep;
        //private Color originalColor;

        // Start is called before the first frame update
        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            ep = GetComponentInParent<EnemyPlatforming>();
            //originalColor = sr.color;
        }

        // Update is called once per frame
        void Update()
        {
            if (ep.canBeDestroyed)
            {
                sr.color = Color.red;
            }
            else
            {
                sr.color = Color.cyan;
            }
        }
    }
}
