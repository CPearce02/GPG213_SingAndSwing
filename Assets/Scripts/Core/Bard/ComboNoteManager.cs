using Enums;
using UnityEngine;

namespace Core.Bard
{
    public class ComboNoteManager : MonoBehaviour
    {
        public ComboValues value;
        private float _speed = 1f;

        private void Start()
        {
        }
        private void Update()
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }
}
