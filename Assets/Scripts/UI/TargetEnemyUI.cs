using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Enemies;

public class TargetEnemyUI : MonoBehaviour
{
    private Enemy _currentTargetEnemy;

    // private Camera _cam;
    [SerializeField] private Canvas _canvas;

    [SerializeField] private GameObject _comboUI;
    [SerializeField] private RectTransform _targetBorder;

    private Vector2 framePadding = new Vector2(20,20);

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        GameEvents.onTargetEnemyEvent += SetTarget;
    }

    private void OnDisable()
    {
        GameEvents.onTargetEnemyEvent -= SetTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTargetEnemy != null && _currentTargetEnemy.CanBeDestroyed)
        {
            _targetBorder.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (_currentTargetEnemy == null) return;
        FollowTarget();
    }

    private void SetTarget(Enemy _targetEnemy)
    {
        if(!_targetEnemy.CanBeDestroyed)
        {
            _currentTargetEnemy = _targetEnemy;
            //Set transform
            // Get the size of the sprite
            Vector2 spriteSize = _targetEnemy.GetComponentInChildren<SpriteRenderer>().sprite.bounds.size;

            // Add padding to the frame size
            Vector2 frameSize = spriteSize + framePadding * 2;

            // Set the size of the frame RectTransform
            _targetBorder.sizeDelta = frameSize;

            //Activate border
            _targetBorder.gameObject.SetActive(true);
        }
    }

    private void FollowTarget()
    {
        Vector3 screenPos = _canvas.worldCamera.WorldToScreenPoint(_currentTargetEnemy.GetComponent<Transform>().position);
        _targetBorder.position = screenPos;
    }
}
