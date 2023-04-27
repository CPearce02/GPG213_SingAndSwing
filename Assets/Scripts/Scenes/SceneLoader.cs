using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneToLoadName = "";

    public void Load()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoadName);
    }
}
