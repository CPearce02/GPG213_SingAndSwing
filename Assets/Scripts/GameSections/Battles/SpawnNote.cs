using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnNote : MonoBehaviour
{
    public GameObject noteToSpawn;
    public float delayTime = 2.0f; // set the delay time in seconds

    private bool canInstantiate = true;
    private Button instantiateButton;

    private BeatScroller bs;

    void Start()
    {
        bs = FindObjectOfType<BeatScroller>();
        instantiateButton = GetComponent<Button>();
        instantiateButton.onClick.AddListener(InstantiatePrefab);
    }

    void InstantiatePrefab()
    {
        if (canInstantiate)
        {
            canInstantiate = false;
            bs.SpawnNote(noteToSpawn);
            StartCoroutine(EnableInstantiate());
        }
    }

    IEnumerator EnableInstantiate()
    {
        yield return new WaitForSeconds(delayTime);
        canInstantiate = true;
    }

}
