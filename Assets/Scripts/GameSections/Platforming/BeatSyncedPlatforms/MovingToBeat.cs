using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using static UnityEditor.Experimental.GraphView.GraphView;
using Core.Player;

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

    public void ToggleMove()
    {
        if(_moved)
        {
            _rb.DOMove(movePos.position, moveTime).SetUpdate(UpdateType.Fixed);
            _moved = false;
            return;
        }

        if(!_moved)
        {
            _rb.DOMove(_initialPos, moveTime).SetUpdate(UpdateType.Fixed);
            _moved = true;
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, movePos.position);
    }
}
