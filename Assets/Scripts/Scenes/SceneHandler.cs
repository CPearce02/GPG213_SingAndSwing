using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public int sceneToLoad;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }
}
