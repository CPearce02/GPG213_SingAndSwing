using Core.Player;
using UnityEngine;

namespace GameSections.Platforming
{
    public class Spring : MonoBehaviour
    {
        public float springHeight = 15f;

        private void OnCollisionEnter2D(Collision2D collision) { if (collision.transform.tag == "Player") collision.transform.GetComponent<PlatformingController>().AddJump(springHeight); }
    }
}
