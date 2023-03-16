using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BeatListener))]
public class MovingToBeat : MonoBehaviour
{
    Vector2 _initialPos;
    [SerializeField] Vector2 movePos;
    [SerializeField] float moveTime;
    BeatListener beatListener;
    bool _moved;

    private void Start()
    {
        _initialPos = transform.position;
        beatListener = GetComponent<BeatListener>();
    }

    private void Update()
    {
        //Makes sure that the object never skips a beat
        if (moveTime > MusicManager.SecondsPerBeat * beatListener.BeatInterval) moveTime = MusicManager.SecondsPerBeat * beatListener.BeatInterval;
    }

    public void ToggleMove()
    {
        if(_moved)
        {
            transform.DOMove(movePos, moveTime);
            _moved = false;
            return;
        }

        if(!_moved)
        {
            transform.DOMove(_initialPos, moveTime);
            _moved = true;
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, movePos);
    }
}
