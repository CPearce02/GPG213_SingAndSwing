using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class LoadCredits : MonoBehaviour
    {
        public void Load()
        {
            StartCoroutine(LoadSequence());
        }

        IEnumerator LoadSequence()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Credits");
        }
    }
}
