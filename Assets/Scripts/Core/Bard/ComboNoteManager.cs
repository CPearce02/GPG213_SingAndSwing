using Enums;
using UnityEngine;

namespace Core.Bard
{
    public class ComboNoteManager : MonoBehaviour
    {
        public ComboValues value;
        private Vector3 _initialPosition;
        private float _speed = 5f;

        private void Start()
        {
            _initialPosition = transform.position;
        }
        private void Update()
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
            if (transform.position.x <= -2f)
            {
                transform.position = _initialPosition;
            }
        }
    }
}
