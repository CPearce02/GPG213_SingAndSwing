using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(_creditsStarted)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);

            if (Input.anyKeyDown == true) QuitCredits();
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