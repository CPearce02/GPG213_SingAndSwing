using Enums;
using UnityEngine;

namespace Core.Bard
{
    public class ComboNoteManager : MonoBehaviour
    {
        public ComboValues value;
        public float _speed;
        //private Vector3 _initialPos;

        private void Start()
        {
            //_initialPos = transform.position;
        }
        private void Update()
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }
}
