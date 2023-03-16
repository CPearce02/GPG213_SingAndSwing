using Events;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class BeatListener : MonoBehaviour
{
    [Tooltip("Toggles this platform, after the set amount of beats have happened")]
    [field: SerializeField] public int BeatInterval { get; private set; } = 1;
    [SerializeField] bool inverted;
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

    private void Start()
    {
        if (inverted) onBeatEvent?.Invoke();
    }

    void AddBeats() => _beatsDone++;

    void CheckBeats()
    {
        AddBeats();

        if (_beatsDone >= BeatInterval)
        {
            onBeatEvent?.Invoke();
            _beatsDone = 0;
        }
    }
}
