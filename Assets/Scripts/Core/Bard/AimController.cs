using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2f;
    private Vector2 aimDirection;
    private Collider2D aimCollider;
    [SerializeField] private PlayerInput _bardInput;

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
        ////Vector2 aimDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        aimDirection = _bardInput.actions["Aim"].ReadValue<Vector2>();

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Rotate the target indicator towards the mouse position
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    }
}
