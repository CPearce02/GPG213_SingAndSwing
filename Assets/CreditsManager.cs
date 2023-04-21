using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] float speed = 10f, waitMoveSeconds;
    RectTransform _transf;
    bool _startMove = false, _startCoroutine = false;

    void Start()
    {
        _transf = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!_startMove)
        {
            if (!_startCoroutine) StartCoroutine(WaitMove());
        }
        else _transf.position = new Vector2(_transf.position.x, _transf.position.y + speed * Time.deltaTime);
    }

    IEnumerator WaitMove()
    {
        _startCoroutine = true;
        yield return new WaitForSeconds(waitMoveSeconds);
        _startMove = true;
    }
}
