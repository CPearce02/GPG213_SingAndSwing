using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class AimController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Transform bard;
    private Vector2 aimDirection;
    private Collider2D aimCollider;
    [SerializeField] private PlayerInput _bardInput;
    private float interpolationSpeed = 5f;

    //private void OnEnable()
    //{
    //    GameEvents.onAimStart += AimTowards;
    //}

    //private void OnDisable()
    //{
    //    GameEvents.onAimStart -= AimTowards;
    //}

    //private void AimTowards(Vector2 direction)
    //{
    //    aimDirection = direction.normalized;
    //    float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

    //    //Rotate the target indicator towards the mouse position
    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    //}
    // Start is called before the first frame update
    void Start()
    {
        aimCollider = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // Vector3 mousePosition = new Vector3(_bardInput.actions["Aim"].ReadValue<Vector2>().x, _bardInput.actions["Aim"].ReadValue<Vector2>().y, 0f);

        //Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(mousePosition - transform.position).normalized);
        Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition - bard.position).normalized);

        Debug.Log(Input.mousePosition);


        //aimDirection = _bardInput.actions["Aim"].ReadValue<Vector2>();

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Rotate the target indicator towards the mouse position
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }
}

