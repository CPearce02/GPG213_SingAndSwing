using Enums;
using UnityEngine;

namespace Core.Bard
{
    public class ComboNoteManager : MonoBehaviour
    {
        public ComboValues value;
        public float _speed;

        private void Start()
        {
        }
        private void Update()
        {
            MoveNote();
        }
        
        private void MoveNote()
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
    }
}
