using Enemies;
using GameSections.Platforming;
using UnityEngine;

namespace Core.Bard
{
    public class EnemySpriteManager : MonoBehaviour
    {
        private SpriteRenderer _sr;
        private Enemy _ep;
        //private Color originalColor;

        // Start is called before the first frame update
        void Start()
        {
            _sr = GetComponent<SpriteRenderer>();
            _ep = GetComponentInParent<Enemy>();
            //originalColor = sr.color;
        }

        // Update is called once per frame
        void Update()
        {
            if (_ep.CanBeDestroyed)
            {
                _sr.color = Color.red;
            }
            else
            {
                _sr.color = Color.cyan;
            }
        }
    }
}
