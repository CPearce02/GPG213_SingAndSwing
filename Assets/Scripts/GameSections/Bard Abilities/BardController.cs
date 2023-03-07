using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Structs;
using Enums;
using UnityEngine.InputSystem;

public class BardController : MonoBehaviour
{
    public bool Grounded { get; private set; }
    public Transform groundCheckTransform;
    public Vector2 groundCheckSize;
    public LayerMask ignoreLayers;

    //particleEvent 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
    }

    private void OnButton1()
    {
        GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue1);
    }
    private void OnButton2()
    {
        GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue2);
    }
    private void OnButton3()
    {
        GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue3);
    }
    private void OnButton4()
    {
        GameEvents.onButtonPressed?.Invoke(comboValue: ComboValues.ComboValue4);
    }
    private void OnAim()
    {
        //GameEvents.onAimStart?.Invoke();
    }

    void GroundCheck() => Grounded = Physics2D.BoxCast(groundCheckTransform.position, groundCheckSize, 0f, Vector2.down, 0.1f, ~ignoreLayers);

    private void OnDrawGizmosSelected() => Gizmos.DrawWireCube(groundCheckTransform.position, groundCheckSize);
}
