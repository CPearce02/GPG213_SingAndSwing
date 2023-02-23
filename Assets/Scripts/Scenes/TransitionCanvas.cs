using UnityEngine;

namespace Scenes
{
    public class TransitionCanvas : MonoBehaviour
    { 
        void Start() => DontDestroyOnLoad(gameObject);
    }
}
