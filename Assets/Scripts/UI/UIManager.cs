using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public string sceneToLoad;
        private void Awake()
        {
            if(sceneToLoad != null)
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        }

    }
}
