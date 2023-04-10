using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SkipIntro : MonoBehaviour
{

    [SerializeField] InputAction button;
    
    [SerializeField] UnityEvent onSkip;

    private void OnEnable()
    {
        button.Enable();
        button.performed += Skip;
    }
    
    private void OnDisable()
    {
        button.Disable();
        button.performed -= Skip;
    }
    
    private void Skip(InputAction.CallbackContext obj)
    { 
        onSkip?.Invoke();
    }

}
