using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Enums;
using UnityEngine.InputSystem;

public class BardController : MonoBehaviour
{
    //particleEvent 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}