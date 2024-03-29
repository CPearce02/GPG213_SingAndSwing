using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class CreditsSequence : MonoBehaviour
    {
        [SerializeField] float speed = 100f, initialWait = 3f, creditsTime = 30f;
        bool _creditsStarted = false;

        private void Start()
        {
            StartCoroutine(WaitForLogo());
        }

        void Update()
        {
            if (_creditsStarted)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);

            }
        }

        IEnumerator WaitForLogo()
        {
            yield return new WaitForSeconds(initialWait);
            _creditsStarted = true;
            yield return new WaitForSeconds(creditsTime);
            SceneManager.LoadScene("MainMenu");
        }

        void QuitCredits()
        {
            StopCoroutine(WaitForLogo());
            SceneManager.LoadScene("MainMenu");
        }
    }
}