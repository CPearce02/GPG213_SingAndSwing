using Events;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    
    private void OnEnable()
    {
        Pause(); 
    }

    private void OnDisable()
    {
        UnPause();
    }

    void Pause()
    {
        Time.timeScale = 0;
        GameEvents.onPauseGame?.Invoke();
    }
    
    void UnPause()
    {
        Time.timeScale = 1;
        GameEvents.onUnPauseGame?.Invoke();
    }
    
    
}
