using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using static UnityEditor.Experimental.GraphView.GraphView;
using Core.Player;
using Events;
using Sound;

[RequireComponent(typeof(BeatListener))] [RequireComponent(typeof(Rigidbody2D))]
public class MovingToBeat : MonoBehaviour
{
    Vector2 _initialPos;
    Rigidbody2D _rb;
    [SerializeField] Transform movePos;
    [SerializeField] float moveTime = 0.2f;
    BeatListener beatListener;
    bool _moved;
    FixedJoint2D joint;
    Tween forward, back;

    private void OnValidate()
    {
        if(movePos == null)
        {
            GameObject directionObj = new GameObject();
            directionObj.transform.SetParent(transform);
            directionObj.transform.position = transform.position;
            directionObj.name = "directionObj";
            movePos = directionObj.transform;
        }
    }

    private void Start()
    {
        _initialPos = transform.position;
        beatListener = GetComponent<BeatListener>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Makes sure that the object never skips a beat
        if (moveTime > MusicManager.SecondsPerBeat * beatListener.BeatInterval) moveTime = MusicManager.SecondsPerBeat * beatListener.BeatInterval;
    }

    private void OnEnable()
    {
        GameEvents.onPlayerFreezeEvent += KillTweens;
    }

    private void OnDisable()
    {
        GameEvents.onPlayerFreezeEvent -= KillTweens;
    }

    public void ToggleMove()
    {
        if(_moved)
        {
            forward = _rb.DOMove(movePos.position, moveTime).SetUpdate(UpdateType.Fixed);
            _moved = false;
            return;
        }

        if(!_moved)
        {
            back = _rb.DOMove(_initialPos, moveTime).SetUpdate(UpdateType.Fixed);
            _moved = true;
            return;
        }
    }

    void KillTweens()
    {
        forward.Kill();
        back.Kill();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, movePos.position);
    }
}
