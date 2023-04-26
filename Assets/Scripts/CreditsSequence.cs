using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSequence : MonoBehaviour
{
    [SerializeField] float speed = 100f, initialWait = 3f;
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
        }
    }

    IEnumerator WaitForLogo()
    {
        yield return new WaitForSeconds(initialWait);
        _creditsStarted = true;
    }
}
