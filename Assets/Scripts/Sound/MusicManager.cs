using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound.ScriptableObjects;
using Events;

public class MusicManager : MonoBehaviour
{
    [SerializeField] MusicData musicData;
    bool _functionFired = false;
    public static float SecondsPerBeat { get; private set; }
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = musicData.musicIntensities[0];
        _audioSource.Play();

        SecondsPerBeat = 60 / musicData.BPM;
    }

    void Update()
    {
        if(!_functionFired) StartCoroutine(FireBeat());
    }

    IEnumerator FireBeat()
    {
        _functionFired = true;
        yield return new WaitForSeconds(SecondsPerBeat);
        //Do something in sync with the beat
        GameEvents.onBeatFiredEvent?.Invoke();
        _functionFired = false;
    }
}
