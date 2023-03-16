using Events;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class BeatListener : MonoBehaviour
{
    [Tooltip("Toggles this platform, after the set amount of beats have happened")]
    [SerializeField] int beatInterval = 1;
    int _beatsDone = 0;
    [SerializeField] UnityEvent onBeatEvent;

    private void OnEnable()
    {
        GameEvents.onBeatFiredEvent += CheckBeats;
    }

    private void OnDisable()
    {
        GameEvents.onBeatFiredEvent -= CheckBeats;
    }

    void AddBeats() => _beatsDone++;

    void CheckBeats()
    {
        AddBeats();

        if (_beatsDone >= beatInterval)
        {
            onBeatEvent?.Invoke();
            _beatsDone = 0;
        }
    }
}
