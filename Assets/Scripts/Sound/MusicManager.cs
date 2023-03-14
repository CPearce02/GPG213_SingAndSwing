using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.ScriptableObjects;

public class MusicManager : MonoBehaviour
{
    [SerializeField] MusicData musicData;
    bool _functionFired = false;
    [SerializeField] float secondsPerBeat;
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = musicData.musicIntensities[0];
        _audioSource.Play();

        secondsPerBeat = 60 / musicData.BPM;
    }

    void Update()
    {
        if(!_functionFired) StartCoroutine(FireBeat());
    }

    IEnumerator FireBeat()
    {
        _functionFired = true;
        yield return new WaitForSeconds(secondsPerBeat);
        //Do something in sync with the beat
        _functionFired = false;
    }
}
